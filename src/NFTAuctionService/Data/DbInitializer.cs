
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
                    Name = "Polar Moose #01",
                    Collection = "Mission: Arctic to Antarctica",
                    IndexInCollection = 1,
                    Tags = "moose, arctic",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/bfe8a505b1f82327b758187172f3e648.png?auto=format&dpr=1&w=1000"
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
                    Name = "Polar Rabbit #01",
                    Collection = "Mission: Arctic to Antarctica",
                    IndexInCollection = 2,
                    Tags = "polar, rabbit, arctic",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/c34c777e01812647f010905f3fa67cc2.png?auto=format&dpr=1&w=1000"
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
                    Name = "Seal #05",
                    Collection = "Mission: Arctic to Antarctica",
                    IndexInCollection = 3,
                    Tags = "seal, polar, arctic",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/01d221864a12ad5c693868aea7c0e0d5.png?auto=format&dpr=1&w=1000"
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
                    Name = "Digital Bear #12",
                    Collection = "Digital Bear in the house",
                    IndexInCollection = 4,
                    Tags = "bear, hero",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/04f250925906b30aaf4548d61a7de88b.png?auto=format&dpr=1&w=1000"
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
                    Name = "Digital Bear #02",
                    Collection = "Digital Bear in the house",
                    IndexInCollection = 5,
                    Tags = "bear, dj, music",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/9937a51c150697b2c2a229dd89a8228a.png?auto=format&dpr=1&w=1000"
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
                    Name = "Digital Bear #11",
                    Collection = "Digital Bear in the house",
                    IndexInCollection = 6,
                    Tags = "bear, warrior",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/12dfd7552e48edaf64b99f57fe8fb477.png?auto=format&dpr=1&w=1000"
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
                    Name = "SolarX08",
                    Collection = "SolarX Garage",
                    IndexInCollection = 7,
                    Tags = "robot",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/a41e788576b129abf6a82fcee539312c.png?auto=format&dpr=1&w=1000"
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
                    Name = "SolarX02",
                    Collection = "SolarX Garage",
                    IndexInCollection = 8,
                    Tags = "robot",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/f5d5bce2d528156245fa42793dff88f0.png?auto=format&dpr=1&w=1000"
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
                    Name = "SolarX09",
                    Collection = "SolarX Garage",
                    IndexInCollection = 9,
                    Tags = "robot",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/abb39bd56b51a28722fcec6f07c1f236.png?auto=format&dpr=1&w=1000"
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
                    Name = "SolarX05",
                    Collection = "SolarX Garage",
                    IndexInCollection = 10,
                    Tags = "robot",
                    Artist = "Baris Ozgen",
                    ContentUrl = "https://i.seadn.io/s/raw/files/8c008b404b29ac0d1663ef4cd0286954.png?auto=format&dpr=1&w=1000"
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
