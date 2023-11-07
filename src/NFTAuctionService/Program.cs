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

