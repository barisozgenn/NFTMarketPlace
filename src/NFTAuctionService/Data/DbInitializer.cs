
using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Entities;

namespace NFTAuctionService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<NFTAuctionDbContext>());
    }

    private static void SeedData(NFTAuctionDbContext context)
    {
        //we'll create the database if it does not already exist.
        context.Database.Migrate();

        if(context.NFTAuctions.Any()){
            // We already have the data no need to seed.
            Console.WriteLine("DEBUG: We already have the data no need to seed.");
            return;
        }

        //demo list
        var nftAuctions = new List<NFTAuction>()
        {
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 100,
                Seller = "Alice",
                Winner = "Baris",
                SoldPrice = 150,
                CurrentHighestBid = 160,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(24),
                Status = NFTAuctionStatus.OnLive,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork1",
                    Collection = "Collection1",
                    IndexInCollection = 1,
                    Tags = "Tag1, Tag2",
                    Artist = "Eve",
                    ContentUrl = "https://example.com/artwork1.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 50,
                Seller = "Carol",
                Winner = null,
                SoldPrice = null,
                CurrentHighestBid = 60,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(48),
                Status = NFTAuctionStatus.Expired,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork2",
                    Collection = "Collection2",
                    IndexInCollection = 2,
                    Tags = "Tag3, Tag4",
                    Artist = "David",
                    ContentUrl = "https://example.com/artwork2.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 75,
                Seller = "Emily",
                Winner = "Frank",
                SoldPrice = 110,
                CurrentHighestBid = 120,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(12),
                Status = NFTAuctionStatus.OnLive,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork3",
                    Collection = "Collection3",
                    IndexInCollection = 3,
                    Tags = "Tag5, Tag6",
                    Artist = "Grace",
                    ContentUrl = "https://example.com/artwork3.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 120,
                Seller = "Hannah",
                Winner = null,
                SoldPrice = null,
                CurrentHighestBid = 130,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(36),
                Status = NFTAuctionStatus.Expired,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork4",
                    Collection = "Collection4",
                    IndexInCollection = 4,
                    Tags = "Tag7, Tag8",
                    Artist = "Ivy",
                    ContentUrl = "https://example.com/artwork4.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 90,
                Seller = "Jack",
                Winner = "Karen",
                SoldPrice = 140,
                CurrentHighestBid = 150,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(18),
                Status = NFTAuctionStatus.OnLive,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork5",
                    Collection = "Collection5",
                    IndexInCollection = 5,
                    Tags = "Tag9, Tag10",
                    Artist = "Liam",
                    ContentUrl = "https://example.com/artwork5.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 60,
                Seller = "Megan",
                Winner = null,
                SoldPrice = null,
                CurrentHighestBid = 70,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(72),
                Status = NFTAuctionStatus.Expired,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork6",
                    Collection = "Collection6",
                    IndexInCollection = 6,
                    Tags = "Tag11, Tag12",
                    Artist = "Nora",
                    ContentUrl = "https://example.com/artwork6.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 80,
                Seller = "Oliver",
                Winner = "Patricia",
                SoldPrice = 95,
                CurrentHighestBid = 100,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(60),
                Status = NFTAuctionStatus.OnLive,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork7",
                    Collection = "Collection7",
                    IndexInCollection = 7,
                    Tags = "Tag13, Tag14",
                    Artist = "Quincy",
                    ContentUrl = "https://example.com/artwork7.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 110,
                Seller = "Rachel",
                Winner = null,
                SoldPrice = null,
                CurrentHighestBid = 120,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(30),
                Status = NFTAuctionStatus.Expired,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork8",
                    Collection = "Collection8",
                    IndexInCollection = 8,
                    Tags = "Tag15, Tag16",
                    Artist = "Samuel",
                    ContentUrl = "https://example.com/artwork8.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 70,
                Seller = "Tracy",
                Winner = "Ursula",
                SoldPrice = 85,
                CurrentHighestBid = 90,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(42),
                Status = NFTAuctionStatus.OnLive,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork9",
                    Collection = "Collection9",
                    IndexInCollection = 9,
                    Tags = "Tag17, Tag18",
                    Artist = "Victor",
                    ContentUrl = "https://example.com/artwork9.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NFTAuction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 65,
                Seller = "William",
                Winner = null,
                SoldPrice = null,
                CurrentHighestBid = 75,
                NFTAuctionEndAt = DateTime.UtcNow.AddHours(54),
                Status = NFTAuctionStatus.Expired,
                Item = new NFTAuctionItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Artwork10",
                    Collection = "Collection10",
                    IndexInCollection = 10,
                    Tags = "Tag19, Tag20",
                    Artist = "Xander",
                    ContentUrl = "https://example.com/artwork10.jpg"
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        //keep the auctions that we add in memory when we use the add range.
        context.AddRange(nftAuctions);

        context.SaveChanges();
    }
}
