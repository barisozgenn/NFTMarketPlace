using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

await DB.InitAsync("SearchNftAuctionDb",
                MongoClientSettings.FromConnectionString(builder.Configuration.
                                                        GetConnectionString("MongoDbBarisDevConnection")));
//create an index for our item for the certain fields that we want to be able to search on.
await DB.Index<NFTAuctionItem>()
        .Key(k => k.Name, KeyType.Text)
        .Key(k => k.Collection, KeyType.Text)
        .Key(k => k.Tags, KeyType.Text)
        .Key(k => k.Artist, KeyType.Text)
        .CreateAsync();

app.Run();

