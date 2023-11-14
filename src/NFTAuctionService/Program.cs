using AuctionService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Consumers;
using NFTAuctionService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<NFTAuctionDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultBarisDevConnection"));
});
//we specify the location of where our mapping profiles are.
//It's going to take a look for any classes that derive from the profile class and register the mappings in memory
//so that when it comes to using Automapper, it's already set up and good to go. inherited from : Profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//We registered mass transit and it is is really the equivalent to of entity framework. We will manage rabbitmq with
builder.Services.AddMassTransit(mst =>
{
    // Adding MassTransit Outbox support for Entity Framework with specified DbContext (NFTAuctionDbContext).
    mst.AddEntityFrameworkOutbox<NFTAuctionDbContext>(outbox =>
    {
        //If the service bus is available, the message will be delivered immediately.
        //But if it's not, then every 10s because of this configuration, it's going to attempt to look inside
        outbox.QueryDelay = TimeSpan.FromSeconds(10);
        //by the way, this mass transit package, it doesn't have a SQL Server option.
        outbox.UsePostgres();
        outbox.UseBusOutbox();
    });
    // Adding consumers for MassTransit from the namespace containing NFTAuctionCreatedFaultConsumer.
    //Faulted message and we can see that we're consuming the faulty creation there, which means it would have put that message back on the bus.
    mst.AddConsumersFromNamespaceContaining<NFTAuctionCreatedFaultConsumer>();
    //it's going to be nftauction-nftauction-created we separated our consumers endpoints
    //we can follow by their name then under RabbitMQ dashboard http://localhost:15672/#/exchanges
    mst.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nftauction", false));
    // Configuring endpoints for the RabbitMQ bus using the provided context.
    mst.UsingRabbitMq((context, configuration) =>
    {
        //Docker can use a different location for Rabbitmq so that it can connect to Rabbitmq when it's running inside a container.
        //we need to add some extra configuration where we're using Rabbitmq.
        //And we're getting this configuration from a rabbitmq section inside our configuration files now.
        configuration.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        configuration.ConfigureEndpoints(context);
    });
});
// We added JwtBearer in our project to use IdentityServer
//and to Authenticate our users because it won't be used without being user
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.Authority = builder.Configuration["IdentityServerBarisDevUrl"];
        options.RequireHttpsMetadata = false;//because our identity server running on http for dev
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

builder.Services.AddGrpc();

var app = builder.Build();
app.UseAuthentication();//If we don't do this, we cannot authenticate, it should be before UseAuthorization
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<GrpcNFTAuctionService>();

try{
    DbInitializer.InitDb(app: app);
}
catch(Exception ex){
    Console.WriteLine(ex);
}

app.Run();

