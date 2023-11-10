using Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class NFTAuctionFinishedConsumer: IConsumer<NFTAuctionFinished>
{
    public async Task Consume(ConsumeContext<NFTAuctionFinished> context)
    {
        var auction = await DB.Find<NFTAuctionItem>().OneAsync(context.Message.NFTAuctionId);

        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldPrice = (int)context.Message.Price;
        }

        auction.Status = "Expired";

        await auction.SaveAsync();
    }
}