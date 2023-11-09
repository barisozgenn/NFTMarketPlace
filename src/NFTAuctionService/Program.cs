using MassTransit;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddMassTransit(x => 
{
    /*x.AddEntityFrameworkOutbox<AuctionDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
    });
    x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));*/
    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

try{
    DbInitializer.InitDb(app: app);
}
catch(Exception ex){
    Console.WriteLine(ex);
}

app.Run();

