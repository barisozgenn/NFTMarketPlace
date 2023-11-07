using System.ComponentModel.DataAnnotations;

namespace NFTAuctionService.DTOs;

public class CreateNFTAuctionDto
{ 
    [Required]
    public string Name { get; set; }
    [Required]
    public string Collection { get; set; }
    [Required]
    public int IndexInCollection { get; set; }
    [Required]
    public string Tags { get; set; }
    [Required]
    public string Artist { get; set; }
    [Required]
    public string ContentUrl { get; set; }
    [Required]
    public int ReservePrice { get; set; }
    [Required]
    public DateTime NFTAuctionEndAt { get; set; }
}
