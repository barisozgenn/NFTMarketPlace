export type PagedResult<T> = {
    results: T[]
    pageCount: number
    totalCount: number
}

export type NFTAuction = {
    reservePrice: number
    seller: string
    winner?: string
    soldPrice: number
    currentHighestBid: number
    nftAuctionEndAt: string
    status: string
    createdAt: string
    updatedAt: string
    name: string
    collection: string
    indexInCollection: number
    tags: string
    artist: number
    contentUrl: string
    id: string
}
/*
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

    public string Name { get; set; }
    public string Collection { get; set; }
    public int IndexInCollection { get; set; }
    public string Tags { get; set; }
    public string Artist { get; set; }
    public string ContentUrl { get; set; }

  */