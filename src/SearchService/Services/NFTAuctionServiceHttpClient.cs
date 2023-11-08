using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class NFTAuctionServiceHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public NFTAuctionServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        // and add http client service url in appsettings NFTAuctionServiceUrl
        // and then we need to register this class in program.cs
        // and then our DB initializer, we are not able to inject anything into list. So change it
        // and then we can add Polly nuget to handle and react any remote http client api failure conditions and define policy in program.cs
    }

    public async Task<List<NFTAuctionItem>> GetItemsToSearchDbFromHttpClient()
    {
        var lastUpdated = await DB.Find<NFTAuctionItem, string>()
            .Sort(x => x.Descending(x => x.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<NFTAuctionItem>>(_configuration["NFTAuctionServiceUrl"]                                                                + "/api/nft-auctions/HttpClient/date=" + lastUpdated);
    }
}
