namespace Contracts.Events;

public class NFTBidPlaced
{
    public string Id { get; set; }
    public string NFTAuctionId { get; set; }
    public string Bidder { get; set; }
    public DateTime BidTime { get; set; }
    public int Price { get; set; }
    public string BidStatus { get; set; }
}
