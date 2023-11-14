using Contracts.Events;
using MassTransit;
using NFTAuctionService.Data;

namespace NFTAuctionService;

public class NFTBidPlacedConsumer: IConsumer<NFTBidPlaced>
{
    private readonly NFTAuctionDbContext _dbContext;

    public NFTBidPlacedConsumer(NFTAuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<NFTBidPlaced> context)
    {
        Console.WriteLine("DEBUG: --> Consuming bid placed");

        var auction = await _dbContext.NFTAuctions.FindAsync(Guid.Parse(context.Message.NFTAuctionId));

        if (auction.CurrentHighestBid == null
            || context.Message.BidStatus.Contains("Accepted")//TODO: check here later
            && context.Message.Price > auction.CurrentHighestBid)
        {
            auction.CurrentHighestBid = context.Message.Price;
            await _dbContext.SaveChangesAsync();
        }
    }
}