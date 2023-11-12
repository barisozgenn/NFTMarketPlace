using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Helpers.Requests;
using SearchService.Models;

namespace SearchService.Controllers;
[ApiController]
[Route("api/search")]
public class SearchController: ControllerBase
{
    //NOTE: if you're wondering why I'm using ActionResult
    //we can quite happily use IActionResult and this will give us the Http responses
    //The only thing with I action result is it doesn't take a type parameter,
    //so I'm not able to do something like this Task<IActionResult<List<NFTAuctionItem>>>, or at least it never used to.
    [HttpGet("nftauctions")]
    //public async Task<ActionResult<List<NFTAuctionItem>>> SearchItems(string searchText,int pageNumber = 1,int pageSize = 4)
    public async Task<ActionResult<List<NFTAuctionItem>>> SearchItems([FromQuery] SearchParameters searchParameters){

        //var query = DB.Find<NFTAuctionItem>();
        //var query = DB.PagedSearch<NFTAuctionItem>();
        //for MongoDB orderby sort usage
        var query = DB.PagedSearch<NFTAuctionItem, NFTAuctionItem>();

        //query.Sort(it => it.Ascending(n => n.Name)); // We do not need anymore we did it below more functional

        if(!string.IsNullOrEmpty(searchParameters.SearchText)){
            query.Match(Search.Full, searchParameters.SearchText).SortByTextScore();
        }

        query = searchParameters.NftOrderBy switch {
            "name" => query.Sort(it => it.Ascending(n => n.Name))
                            .Sort(it => it.Ascending(n => n.Collection)),
            "newest" => query.Sort(it => it.Descending(n => n.Name)),
            //Default parameter
            _ => query.Sort(it => it.Ascending(n => n.NFTAuctionEndAt)),
        };

        query = searchParameters.NftFilterBy switch {
            "expired" => query.Match(it => it.NFTAuctionEndAt < DateTime.UtcNow),
            "expiringSoon" => query.Match(it => it.NFTAuctionEndAt < DateTime.UtcNow.AddHours(12)
                                                && it.NFTAuctionEndAt > DateTime.UtcNow),
            //Default parameter
            _ => query.Match(it => it.NFTAuctionEndAt > DateTime.UtcNow)
        };

        if(!string.IsNullOrEmpty(searchParameters.NftCollection))
            query.Match(it => it.Collection == searchParameters.NftCollection);

        if(!string.IsNullOrEmpty(searchParameters.NftArtist))
            query.Match(it => it.Artist == searchParameters.NftArtist);

        if(!string.IsNullOrEmpty(searchParameters.NftSeller))
            query.Match(it => it.Seller == searchParameters.NftSeller);

        query.PageNumber(pageNumber: searchParameters.PageNumber);
        query.PageSize(pageSize: searchParameters.PageSize);

        var results = await query.ExecuteAsync();

        //return results;
        return Ok(new {
            results = results.Results,
            pageCount = results.PageCount,
            totalCount = results.TotalCount
        });
    }
}
