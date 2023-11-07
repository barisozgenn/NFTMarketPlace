﻿using NFTAuctionService.Entities;

namespace NFTAuctionService;

public class NFTAuctionItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Collection { get; set; }
    public int IndexInCollection { get; set; }
    public string Tags { get; set; }
    public string Artist { get; set; }
    public string ContentUrl { get; set; }

    // nav properties

    public Guid NFTAuctionId { get; set; }
    public NFTAuction NFTAuction { get; set; }
}
