using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

public class NFTBidPlacedConsumer : IConsumer<NFTBidPlaced>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NFTBidPlacedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<NFTBidPlaced> context)
    {
        Console.WriteLine("DEBUG: --> bid placed message received");

        await _hubContext.Clients.All.SendAsync("NFTBidPlaced", context.Message);
    }
}


