using FitWallet.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitWallet.Database;
using FitWallet.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;

namespace FitWallet.Api.Controllers;

[Authorize]
public class WalletsController : ApplicationControllerBase
{
    private readonly ApplicationDatabaseContext _context;

    public WalletsController(
        ILogger logger,
        IMapper mapper,
        ApplicationDatabaseContext context) : base(logger, mapper)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<Ok<IEnumerable<WalletDto>>> GetWallets()
    {
        var userId = GetUserId();

        var wallets = await _context.Wallets
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .ToListAsync();

        var walletsDtos = Mapper.Map<List<Wallet>, IEnumerable<WalletDto>>(wallets);

        return TypedResults.Ok(walletsDtos);
    }

    [HttpGet("{id}")]
    public async Task<Results<Ok<WalletDto>, NotFound>> GetWallet(string id)
    {
        var userId = GetUserId();

        var wallet = await _context.Wallets
            .AsNoTracking()
            .FirstOrDefaultAsync(e => 
                e.UserId == userId && 
                e.Id == id);

        if (wallet is null)
        {
            return TypedResults.NotFound();
        }

        var walletDto = Mapper.Map<Wallet, WalletDto>(wallet);

        return TypedResults.Ok(walletDto);
    }

    [HttpPut("{id}")]
    public async Task<Results<NotFound, NoContent, Conflict<string>>> UpdateWallet(string id, [FromBody] WalletDto request)
    {
        var userId = GetUserId();

        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(e => 
                e.Id == id && 
                e.UserId == userId);

        if (wallet is null)
        {
            return TypedResults.NotFound();
        }

        if (await _context.Wallets.AnyAsync(e => e.Name == wallet.Name && e.UserId == userId))
        {
            return TypedResults.Conflict("There is already a wallet with this name");
        }

        Mapper.Map(request, wallet);

        if (request.Disabled is not null)
        {
            wallet.Disabled = request.Disabled.Value;
        }

        await _context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [HttpPost]
    public async Task<Results<Conflict<string>,Ok<Wallet>>> AddWallet([FromBody] WalletDto request)
    {
        var userId = GetUserId();

        if (await _context.Wallets.AnyAsync(e => e.Name == request.Name && e.UserId == userId))
        {
            return TypedResults.Conflict("There is already a wallet with this name");
        }

        var wallet = new Wallet
        {
            Disabled = false,
            UserId = userId
        };

        Mapper.Map(request, wallet);

        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();

        return TypedResults.Ok(wallet);
    }

    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> DeleteWallet(string id)
    {
        var userId = GetUserId();

        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(e => 
                e.Id == id && 
                e.UserId == userId);

        if (wallet is null)
        {
            return TypedResults.NotFound();
        }

        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}