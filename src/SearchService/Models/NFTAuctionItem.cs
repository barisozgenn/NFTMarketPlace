using MongoDB.Entities;

namespace SearchService.Models;

public class NFTAuctionItem: Entity
{
    // We do not need to define ID because it is coming from mongodb Entity
    //public string ID { get; set; }
    public int ReservePrice { get; set; }
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int SoldPrice { get; set; }
    public int CurrentHighestBid { get; set; }
    public DateTime NFTAuctionEndAt { get; set; }
    public string Status  { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // NFTAuctionItem
    public string Name { get; set; }
    public string Collection { get; set; }
    public int IndexInCollection { get; set; }
    public string Tags { get; set; }
    public string Artist { get; set; }
    public string ContentUrl { get; set; }
}
