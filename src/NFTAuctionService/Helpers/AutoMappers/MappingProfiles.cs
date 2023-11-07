using AutoMapper;
using NFTAuctionService.DTOs;
using NFTAuctionService.Entities;

namespace NFTAuctionService.Helpers.AutoMappers;

public class MappingProfiles: Profile // AutoMapper
{
    public MappingProfiles()
    {
        CreateMap<NFTAuction,NFTAuctionDto>().IncludeMembers(nftA => nftA.Item); // Add NFTAuctionItem to NFTAuction
        CreateMap<NFTAuctionItem,NFTAuctionDto>();
        CreateMap<CreateNFTAuctionDto, NFTAuction>()
            .ForMember(nftA => nftA.Item, o => o.MapFrom(nft => nft)); // Because we will also add NFTAuctionItem to NFTAuction
        CreateMap<CreateNFTAuctionDto, NFTAuctionItem>();
    }
    //also its dependency added program.cs
}
