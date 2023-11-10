using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFTAuctionService.Data;
using NFTAuctionService.DTOs;
using NFTAuctionService.Entities;

namespace NFTAuctionService.Controller;
[ApiController]
[Route("api/nftauctions")]
public class NFTAuctionsController: ControllerBase
{
    private readonly NFTAuctionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public NFTAuctionsController(NFTAuctionDbContext context, IMapper mapper, 
                                IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<NFTAuctionDto>>> GetAll(){

        var nftAuctions = await _context.NFTAuctions
            .Include(nftAuction => nftAuction.Item)
            .OrderBy(nftAuction => nftAuction.Item.Name)
            .ToListAsync();

        return _mapper.Map<List<NFTAuctionDto>>(nftAuctions);
    }
    [HttpGet("httpClient")]
    // We will call this service from SearchService/Services/NFTAuctionServiceHttpClient to test httpclient
    public async Task<ActionResult<List<NFTAuctionDto>>> GetAllWithHttpRequest([FromQuery]string date){

        var query = _context.NFTAuctions.OrderBy(i => i.Item.Name).AsQueryable();
        if(!string.IsNullOrEmpty(date)){
            query = query.Where(i => i.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) < 0);
        }

        return await query.ProjectTo<NFTAuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
    [HttpGet("byId/{id}")]
    public async Task<ActionResult<NFTAuctionDto>> GetById(Guid id){

        var nftAuction = await _context.NFTAuctions
            .Include(nftA => nftA.Item)
            .FirstOrDefaultAsync(nftA => nftA.Id == id);

        if(nftAuction == null) return NotFound();

        return _mapper.Map<NFTAuctionDto>(nftAuction);
    }

    [Authorize]//We don't want anonymous users, for example, to be able to create,update,delete
    [HttpPost]
    public async Task<ActionResult<NFTAuctionDto>> CreateNFTAuction(CreateNFTAuctionDto nftAuctionDto){

        var nftAuction = _mapper.Map<NFTAuction>(nftAuctionDto);//Map from CreatedNFTAuctionDto tp NFTAuction

        nftAuction.Seller = User.Identity.Name;//that's going to give us the username.

        _context.NFTAuctions.Add(nftAuction);

        var newNftAuction = _mapper.Map<NFTAuctionDto>(nftAuction);
        //publish it to service bus rabbitmq
        await _publishEndpoint.Publish(_mapper.Map<NFTAuctionCreated>(newNftAuction));

        var result = await _context.SaveChangesAsync() > 0;

        if(!result) return BadRequest("Could not save changes to the DB");

        return  CreatedAtAction(nameof(GetById),//call method name
                                new {nftAuction.Id},//called method's parameter
                                newNftAuction);//returned result
    }
    /*NOT: without service bus or rabbitmq version
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
    */

    [Authorize]//We don't want anonymous users, for example, to be able to create,update,delete
    [HttpPut("byId/{id}")]
    public async Task<ActionResult> UpdateNFTAuction(Guid id, UpdateNFTAuctionDto nftAuctionDto){

        var nftAuction = await _context.NFTAuctions.Include(it => it.Item)
                                                    .FirstOrDefaultAsync(it => it.Id == id);
        if(nftAuction == null) return NotFound();
        if(nftAuction.Seller != User.Identity.Name) return Forbid();//for http 403 response

        //TODO: create a control system for catching what parameter user changes and we need to only update them
        nftAuction.Item.Name = nftAuctionDto.Name ?? nftAuction.Item.Name;
        nftAuction.Item.Tags = nftAuctionDto.Tags ?? nftAuction.Item.Tags;
        //publish it to service bus
        await _publishEndpoint.Publish(_mapper.Map<NFTAuctionUpdated>(nftAuction));

        var result = await _context.SaveChangesAsync() > 0;

        if(result) return Ok();
        return BadRequest("Some errors during saving changes");
    }
    [Authorize]//We don't want anonymous users, for example, to be able to create,update,delete
    [HttpDelete("byId/{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.NFTAuctions.FindAsync(id);

        if (auction == null) return NotFound();

        if (auction.Seller != User.Identity.Name) return Forbid();//for http 403 response

        _context.NFTAuctions.Remove(auction);
        //publish it to service bus
        await _publishEndpoint.Publish<NFTAuctionDeleted>(new { Id = auction.Id.ToString() });

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not update DB");

        return Ok();
    }
}
