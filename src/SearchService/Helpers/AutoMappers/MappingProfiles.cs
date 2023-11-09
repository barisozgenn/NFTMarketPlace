using AutoMapper;
using Contracts.Events;
using SearchService.Models;

namespace SearchService.Helpers.AutoMappers;

public class MappingProfiles: Profile // AutoMapper
{
    public MappingProfiles()
    {
        CreateMap<NFTAuctionCreated, NFTAuctionItem>();
    }
}
