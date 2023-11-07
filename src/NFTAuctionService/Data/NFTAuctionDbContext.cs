using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Entities;

namespace NFTAuctionService.Data;

public class NFTAuctionDbContext : DbContext
{
    public NFTAuctionDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<NFTAuction> NFTAuctions { get; set; }
}
