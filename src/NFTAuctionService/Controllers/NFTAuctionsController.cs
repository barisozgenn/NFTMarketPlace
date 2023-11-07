using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Data;
using NFTAuctionService.DTOs;

namespace NFTAuctionService.Controller;
[ApiController]
[Route("api/nft-auctions")]
public class NFTAuctionsController: ControllerBase
{
    private readonly NFTAuctionDbContext _context;
    private readonly IMapper _mapper;

    public NFTAuctionsController(NFTAuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<NFTAuctionDto>>> GetAll(){

        var nftAuctions = await _context.NFTAuctions
            .Include(nftAuction => nftAuction.Item)
            .OrderBy(nftAuction => nftAuction.Item.Name)
            .ToListAsync();

        return _mapper.Map<List<NFTAuctionDto>>(nftAuctions);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<NFTAuctionDto>> GetById(Guid id){

        var nftAuction = await _context.NFTAuctions
            .Include(nftA => nftA.Item)
            .FirstOrDefaultAsync(nftA => nftA.Id == id);

        if(nftAuction == null) return NotFound();

        return _mapper.Map<NFTAuctionDto>(nftAuction);
    }
}
