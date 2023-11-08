using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app){
        await DB.InitAsync("SearchNftAuctionDb",
                MongoClientSettings.FromConnectionString(app.Configuration.
                                                        GetConnectionString("MongoDbBarisDevConnection")));//mongodb://username:password@hostname:port
        //create an index for our item for the certain fields that we want to be able to search on.
        await DB.Index<NFTAuctionItem>()
                .Key(k => k.Name, KeyType.Text)
                .Key(k => k.Collection, KeyType.Text)
                .Key(k => k.Tags, KeyType.Text)
                .Key(k => k.Artist, KeyType.Text)
                .CreateAsync();

        var nftItemsCount = await DB.CountAsync<NFTAuctionItem>();
        if(nftItemsCount == 0){
            Console.WriteLine("DEBUG: no data seed, DbInitializer will seed them");
            var nftItemsData = await File.ReadAllTextAsync("Data/nftauctions.json");
            var jsonOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true };
            var nftItems = JsonSerializer.Deserialize<List<NFTAuctionItem>>(nftItemsData,options: jsonOptions);

            await DB.SaveAsync(nftItems);
        }
    }
}
