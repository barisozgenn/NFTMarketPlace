using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

public class NFTAuctionFinishedConsumer : IConsumer<NFTAuctionFinished>
{ 
    private readonly IHubContext<NotificationHub> _hubContext;

    public NFTAuctionFinishedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<NFTAuctionFinished> context)
    {
        Console.WriteLine("DEBUG: --> nft auction finished message received");

        await _hubContext.Clients.All.SendAsync("NFTAuctionFinished", context.Message);
    }
}
