using MassTransit;
using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Entities;

namespace NFTAuctionService.Data;

public class NFTAuctionDbContext : DbContext
{
    public NFTAuctionDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<NFTAuction> NFTAuctions { get; set; }

    //this is going to be responsible for our outbox functionality on our databases also
    //dotnet ef migrations add Outbox
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
