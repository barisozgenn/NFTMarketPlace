namespace Contracts.Events;

public class NFTAuctionFinished
{
    public bool ItemSold { get; set; }
    public string NFTAuctionId { get; set; }
    public string Winner { get; set; }
    public string Seller { get; set; }
    public int? Price { get; set; }
}
