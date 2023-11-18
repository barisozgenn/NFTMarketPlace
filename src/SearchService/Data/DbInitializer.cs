using System.Text.Json;
using Microsoft.OpenApi.Writers;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public class DbInitializer
{
    //OPINION: in a real big enterprise level application, you wouldn't be using MongoDB to provide the search 
    //You would be using something different with better technologies to go out and crawl the content that you want to index and put in that database.
    public static async Task InitDb(WebApplication app){
        await DB.InitAsync("SearchNftAuctioDb",
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

        //FOR: test db initializer
        /*if(nftItemsCount == 0){
            Console.WriteLine("DEBUG: no data seed, DbInitializer will seed them");
            var nftItemsData = await File.ReadAllTextAsync("Data/nftauctions.json");
            var jsonOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true };
            var nftItems = JsonSerializer.Deserialize<List<NFTAuctionItem>>(nftItemsData,options: jsonOptions);

            await DB.SaveAsync(nftItems);
        }*/
        //FOR: http client initializer
        using var scope = app.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<NFTAuctionServiceHttpClient>();
        var items = await httpClient.GetItemsToSearchDbFromHttpClient();
        Console.WriteLine("DEBUG: returned from http nft auction service: "+items.Count);
        if(items.Count != nftItemsCount)await DB.SaveAsync(items);
    }
}
