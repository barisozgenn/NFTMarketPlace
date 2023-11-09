using Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class NFTAuctionDeletedConsumerIConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<NFTAuctionDeleted> context)
    {
        Console.WriteLine("DEBUG --> Consuming nftauctionDELETED_name:("+context.Message.Id+") created");

        var result = await DB.DeleteAsync<NFTAuctionItem>(context.Message.Id);

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(AuctionDeleted), "Error: deleting nft auction");
    }
}