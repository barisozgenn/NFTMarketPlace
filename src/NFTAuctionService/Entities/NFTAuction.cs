namespace NFTAuctionService.Entities;

public class NFTAuction
{ 
    public Guid Id { get; set; }
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int? SoldPrice { get; set; }
    public int? CurrentHighestBid { get; set; }
    public DateTime NFTAuctionEndAt { get; set; } = DateTime.UtcNow;
    public NFTAuctionStatus Status  { get; set; }
    public NFTAuctionItem Item  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
