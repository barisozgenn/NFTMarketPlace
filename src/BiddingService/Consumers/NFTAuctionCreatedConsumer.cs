namespace BiddingService.Consumers;

using BiddingService.Models;
using Contracts.Events;
using MassTransit;
using MongoDB.Entities;
public class NFTAuctionCreatedConsumer : IConsumer<NFTAuctionCreated>
{
    public async Task Consume(ConsumeContext<NFTAuctionCreated> context)
    {
        var nftAuction = new NFTAuction
        {
            ID = context.Message.Id.ToString(),
            Seller = context.Message.Seller,
            NFTAuctionEndAt = context.Message.NFTAuctionEndAt,
            ReservePrice = context.Message.ReservePrice
        };

        await nftAuction.SaveAsync();
    }
}