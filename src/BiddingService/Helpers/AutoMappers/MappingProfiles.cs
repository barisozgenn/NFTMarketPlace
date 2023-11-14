using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts.Events;

namespace BiddingService.Helpers.AutoMappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Bid, BidDto>();
        CreateMap<Bid, NFTBidPlaced>();
    }
}