using Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class NFTBidPlacedConsumer : IConsumer<NFTBidPlaced>
{
    //And inside here, we don't need to inject anything because we're using MongoDB inside our search service.
    //Actually here totally feeding by NFTAuctionService > Rabbitmq
    public async Task Consume(ConsumeContext<NFTBidPlaced> context)
    {
        Console.WriteLine("DUBEG: --> Consuming bid placed");

        var auction = await DB.Find<NFTAuctionItem>().OneAsync(context.Message.NFTAuctionId);

        if (context.Message.BidStatus.Contains("Accepted")//TODO: check it later dor accepted text
            && context.Message.Price > auction.CurrentHighestBid)
        {
            auction.CurrentHighestBid = context.Message.Price;
            await auction.SaveAsync();
        }
    }
}
