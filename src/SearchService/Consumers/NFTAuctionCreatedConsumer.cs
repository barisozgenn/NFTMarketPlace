using AutoMapper;
using Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class NFTAuctionCreatedConsumer : IConsumer<NFTAuctionCreated>
{
    private readonly IMapper _mapper;

    public NFTAuctionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<NFTAuctionCreated> context)
    {
        Console.WriteLine("DEBUG --> Consuming nftauction_name:("+context.Message.Name+") created");
        Console.WriteLine("DEBUG --> Consuming nftauction_id:("+context.Message.Id+") created");

        var nftItem = _mapper.Map<NFTAuctionItem>(context.Message);
        //let's simply test how can we deal with error, it is not for real life but just for us understanding
        if (nftItem.Name == "Foo") throw new ArgumentException("Cannot sell nft with name of Foo");

        await nftItem.SaveAsync();
    }
}
