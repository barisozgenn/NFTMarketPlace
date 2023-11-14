using MongoDB.Entities;

namespace BiddingService.Models;

public class NFTAuction : Entity
{
    public DateTime NFTAuctionEndAt { get; set; }
    public string Seller { get; set; }
    public int ReservePrice { get; set; }
    public bool Expired { get; set; }
}