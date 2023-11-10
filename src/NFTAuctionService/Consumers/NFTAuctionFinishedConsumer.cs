using Contracts.Events;
using MassTransit;
using NFTAuctionService.Data;
using NFTAuctionService.Entities;


namespace NFTAuctionService.Consumers;

public class NFTAuctionFinishedConsumer: IConsumer<NFTAuctionFinished>
{
    private readonly NFTAuctionDbContext _dbContext;

    public NFTAuctionFinishedConsumer(NFTAuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<NFTAuctionFinished> context)
    {
        Console.WriteLine("DEBUG: --> Consuming auction finished");

        var nftAuction = await _dbContext.NFTAuctions.FindAsync(context.Message.NFTAuctionId);

        if (context.Message.ItemSold)
        {
            nftAuction.Winner = context.Message.Winner;
            nftAuction.SoldPrice = context.Message.Price;
        }

        nftAuction.Status = nftAuction.SoldPrice > nftAuction.ReservePrice
            ? NFTAuctionStatus.Expired : NFTAuctionStatus.MinimumBidNotReached;

        await _dbContext.SaveChangesAsync();
    }
}
