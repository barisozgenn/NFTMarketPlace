using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService;
using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//prodive mapping profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//register NFTAuctionServiceHttpClient
builder.Services.AddHttpClient<NFTAuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());
//We registered mass transit and it is is really the equivalent to of entity framework. We will manage rabbitmq with
builder.Services.AddMassTransit(mst =>
{
    //any other consumers we create in this same namespace are automatically going to be registered by mass transit.
    mst.AddConsumersFromNamespaceContaining<NFTAuctionCreatedConsumer>();
    //it's going to be search-nftauction-created we separated our consumers endpoints
    //we can follow by their name then under RabbitMQ dashboard http://localhost:15672/#/exchanges
    mst.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    mst.UsingRabbitMq((context, configuration) =>
    {
        // Setting up a RabbitMQ message consumer for the "search-auction-created" endpoint.
        configuration.ReceiveEndpoint("search-auction-created", enf =>
        {
            //define the number of retries and the time in between the retries.
            enf.UseMessageRetry(r => r.Interval(5, 5));
            //which consumer we're configuring this for
            enf.ConfigureConsumer<NFTAuctionCreatedConsumer>(context);
        });
        // Configuring endpoints for the RabbitMQ bus using the provided context.
        configuration.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

//let move DB initializer code to DbInitializer.cs
/*FOR: db init dependencies here
await DB.InitAsync("SearchNftAuctionDb",
                MongoClientSettings.FromConnectionString(builder.Configuration.
                                                        GetConnectionString("MongoDbBarisDevConnection")));//mongodb://username:password@hostname:port
//create an index for our item for the certain fields that we want to be able to search on.
await DB.Index<NFTAuctionItem>()
        .Key(k => k.Name, KeyType.Text)
        .Key(k => k.Collection, KeyType.Text)
        .Key(k => k.Tags, KeyType.Text)
        .Key(k => k.Artist, KeyType.Text)
        .CreateAsync();
*/
//FOR: db init dependencies in DBInitializer.cs
/*try{
    await DbInitializer.InitDb(app: app);
}
catch(Exception ex){
    Console.WriteLine(ex);
}*/
//FOR: db init dependencies in DBInitializer.cs
//and we've got a bit of resiliency with our not good practice Http service to go and get the data
//even though the remote NftAuctionService is down, we've kind of removed a bit of dependency on that.
app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
});
app.Run();

//we added Polly nuget to handle and react any remote http client api failure conditions
static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
