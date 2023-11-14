using Grpc.Core;
using NFTAuctionService;
using NFTAuctionService.Data;

namespace AuctionService.Services;

//We will use GrpcNFTAuction.GrpcNFTAuctionBase these classes which are automatically created 
//when we build first time after adding proto files and changing appsettings json
//without build you can get missing namespace error.
public class GrpcNFTAuctionService : GrpcNFTAuction.GrpcNFTAuctionBase
{
    private readonly NFTAuctionDbContext _dbContext;

    public GrpcNFTAuctionService(NFTAuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<GrpcNFTAuctionResponse> GetNFTAuction(GetNFTAuctionRequest request, 
        ServerCallContext context) 
    {
        Console.WriteLine("==> Received Grpc request for auction");

        var auction = await _dbContext.NFTAuctions.FindAsync(Guid.Parse(request.Id)) 
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
        var response = new GrpcNFTAuctionResponse
        {
            NftAuction = new GrpcNFTAuctionModel
            {
                NftAuctionEndAt = auction.NFTAuctionEndAt.ToString(),
                Id = auction.Id.ToString(),
                ReservePrice = auction.ReservePrice,
                Seller = auction.Seller
            }
        };

        return response;
    }
}