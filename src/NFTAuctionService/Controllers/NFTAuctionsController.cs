using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Data;
using NFTAuctionService.DTOs;
using NFTAuctionService.Entities;

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
    [HttpGet("/HttpClient/{date}")]
    // We will call this service from SearchService/Services/NFTAuctionServiceHttpClient to test httpclient
    public async Task<ActionResult<List<NFTAuctionDto>>> GetAllWithHttpRequest(string date){

        var query = _context.NFTAuctions.OrderBy(i => i.Item.Name).AsQueryable();
        if(!string.IsNullOrEmpty(date)){
            query = query.Where(i => i.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        return await query.ProjectTo<NFTAuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<NFTAuctionDto>> GetById(Guid id){

        var nftAuction = await _context.NFTAuctions
            .Include(nftA => nftA.Item)
            .FirstOrDefaultAsync(nftA => nftA.Id == id);

        if(nftAuction == null) return NotFound();

        return _mapper.Map<NFTAuctionDto>(nftAuction);
    }

    [HttpPost]
    public async Task<ActionResult<NFTAuctionDto>> CreateNFTAuction(CreateNFTAuctionDto nftAuctionDto){

        var nftAuction = _mapper.Map<NFTAuction>(nftAuctionDto);//Map from CreatedNFTAuctionDto tp NFTAuction
        //TODO: add current user as seller
        nftAuction.Seller = "test baris seller";

        _context.NFTAuctions.Add(nftAuction);
        var result = await _context.SaveChangesAsync() > 0;

        if(!result) return BadRequest("Could not save changes to the DB");

        return  CreatedAtAction(nameof(GetById),//call method name
                                new {nftAuction.Id},//called method's parameter
                                _mapper.Map<NFTAuctionDto>(nftAuction));//returned result
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateNFTAuction(Guid id, UpdateNFTAuctionDto nftAuctionDto){

        var nftAuction = await _context.NFTAuctions.Include(it => it.Item)
                                                    .FirstOrDefaultAsync(it => it.Id == id);
        if(nftAuction == null) return NotFound();
        //TODO: check seller == username

        //TODO: create a control system for catching what parameter user changes and we need to only update them
        nftAuction.Item.Name = nftAuctionDto.Name ?? nftAuction.Item.Name;
        nftAuction.Item.Tags = nftAuctionDto.Tags ?? nftAuction.Item.Tags;

        var result = await _context.SaveChangesAsync() > 0;

        if(result) return Ok();
        return BadRequest("Some errors during saving changes");
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.NFTAuctions.FindAsync(id);

        if (auction == null) return NotFound();

        if (auction.Seller != User.Identity.Name) return Forbid();

        _context.NFTAuctions.Remove(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not update DB");

        return Ok();
    }
}
