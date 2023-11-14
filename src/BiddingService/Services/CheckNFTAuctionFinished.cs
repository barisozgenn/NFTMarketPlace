using BiddingService.Models;
using Contracts.Events;
using MassTransit;
using MongoDB.Entities;

namespace BiddingService.Services;

public class CheckNFTAuctionFinished: BackgroundService
{private readonly ILogger<CheckNFTAuctionFinished> _logger;
    private readonly IServiceProvider _services;

    public CheckNFTAuctionFinished(ILogger<CheckNFTAuctionFinished> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting check for finished auctions");
//And we get this cancellation token here and this is triggered when stop async is called 
//and we can make use of the stopping token to stop execution of any running requests.
//When this is provided, such as our application shuts down, then we want to stop any database activity
//and we can use this cancellation token to do that.
        stoppingToken.Register(() => _logger.LogInformation("==> Auction check is stopping"));
//So as long as this cancellation has not been requested
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckAuctions(stoppingToken);

            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task CheckAuctions(CancellationToken stoppingToken)
    {
        var finishedAuctions = await DB.Find<NFTAuction>()
            .Match(x => x.NFTAuctionEndAt <= DateTime.UtcNow)
            .Match(x => !x.Expired)
            .ExecuteAsync(stoppingToken);
        
        if (finishedAuctions.Count == 0) return;

        _logger.LogInformation("==> Found {count} auctions that have completed", finishedAuctions.Count);

        using var scope = _services.CreateScope();
        var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        foreach (var auction in finishedAuctions)
        {
            auction.Expired = true;
            await auction.SaveAsync(null, stoppingToken);

            var winningBid = await DB.Find<Bid>()
                .Match(a => a.NFTAuctionId == auction.ID)
                .Match(b => b.BidStatus == BidStatus.Accepted)
                .Sort(x => x.Descending(s => s.Price))
                .ExecuteFirstAsync(stoppingToken);

            await endpoint.Publish(new NFTAuctionFinished
            {
                ItemSold = winningBid != null,
                NFTAuctionId = auction.ID,
                Winner = winningBid?.Bidder,
                Price = winningBid?.Price,
                Seller = auction.Seller
            }, stoppingToken);
        }
    }
}