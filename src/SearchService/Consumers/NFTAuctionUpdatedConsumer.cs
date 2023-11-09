using AutoMapper;
using Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class NFTAuctionUpdatedConsumer: IConsumer<NFTAuctionUpdated>
{
    private readonly IMapper _mapper;

    public NFTAuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<NFTAuctionUpdated> context)
    {
        Console.WriteLine("DEBUG --> Consuming nftauctionUPDATED_name:("+context.Message.Name+") created");
        Console.WriteLine("DEBUG --> Consuming nftauctionUPDATED_id:("+context.Message.Id+") created");

        var item = _mapper.Map<NFTAuctionItem>(context.Message);

        var result = await DB.Update<NFTAuctionItem>()
            .Match(a => a.ID == context.Message.Id)
            .ModifyOnly(x => new
            {
                x.Name,
                x.IndexInCollection,
                x.Tags,
                x.ReservePrice,
                x.NFTAuctionEndAt
            }, item)
            .ExecuteAsync();

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(NFTAuctionUpdated), "Problem updating mongodb");
    }
}