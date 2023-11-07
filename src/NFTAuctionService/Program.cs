using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<NFTAuctionDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultBarisDevConnection"));
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();

