using BiddingService.Models;
using Grpc.Net.Client;
using NFTAuctionService;

namespace BiddingService.Services;

public class GrpcAuctionClient
{
private readonly ILogger<GrpcAuctionClient> _logger;
    private readonly IConfiguration _config;

    public GrpcAuctionClient(ILogger<GrpcAuctionClient> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public NFTAuction GetNFTAuction(string id)
    {
        _logger.LogInformation("Calling GRPC Service");
        var channel = GrpcChannel.ForAddress(_config["GrpcNFTAuction"]);
        var client = new GrpcNFTAuction.GrpcNFTAuctionClient(channel);
        var request = new GetNFTAuctionRequest{Id = id};

        try
        {
            var reply = client.GetNFTAuction(request);
            var auction = new NFTAuction
            {
                ID = reply.NftAuction.Id,
                NFTAuctionEndAt = DateTime.Parse(reply.NftAuction.NftAuctionEndAt),
                Seller = reply.NftAuction.Seller,
                ReservePrice = reply.NftAuction.ReservePrice
            };

            return auction;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not call GRPC Server");
            return null;
        }
    }
}
