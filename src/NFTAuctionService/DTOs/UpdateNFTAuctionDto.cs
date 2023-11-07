namespace NFTAuctionService.DTOs;

public class UpdateNFTAuctionDto
{
    public string Name { get; set; }
    public int IndexInCollection { get; set; }
    public string Tags { get; set; }
    public int ReservePrice { get; set; }
    public DateTime NFTAuctionEndAt { get; set; }
}
