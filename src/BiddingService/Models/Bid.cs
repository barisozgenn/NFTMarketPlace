using MongoDB.Entities;

namespace BiddingService.Models;

public class Bid : Entity
{
    public string NFTAuctionId { get; set; }
    public string Bidder { get; set; }
    public DateTime BidDate { get; set; } = DateTime.UtcNow;
    public int Price { get; set; }
    public BidStatus BidStatus { get; set; }
}