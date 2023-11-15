using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

public class NFTAuctionCreatedConsumer : IConsumer<NFTAuctionCreated>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NFTAuctionCreatedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<NFTAuctionCreated> context)
    {
        Console.WriteLine("DEBUG: --> nft auction created message received");

        await _hubContext.Clients.All.SendAsync("NFTAuctionCreated", context.Message);
    }
}
