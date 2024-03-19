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
public class WalletsController(
		ILogger<WalletsController> logger,
		IMapper mapper,
		ApplicationDatabaseContext dbContext) : ApplicationControllerBase(logger, mapper, dbContext)
{
	[HttpGet]
    public async Task<Ok<IEnumerable<WalletDto>>> GetWallets()
    {
        var userId = GetUserId();

        var wallets = await _dbContext.Wallets
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .ToListAsync();

        var walletsDtos = _mapper.Map<List<Wallet>, IEnumerable<WalletDto>>(wallets);

        return TypedResults.Ok(walletsDtos);
    }

    [HttpGet("{id}")]
    public async Task<Results<Ok<WalletDto>, NotFound>> GetWallet(string id)
    {
        var userId = GetUserId();

        var wallet = await _dbContext.Wallets
            .AsNoTracking()
            .FirstOrDefaultAsync(e => 
                e.UserId == userId && 
                e.Id == id);

        if (wallet is null)
        {
            return TypedResults.NotFound();
        }

        var walletDto = _mapper.Map<Wallet, WalletDto>(wallet);

        return TypedResults.Ok(walletDto);
    }

    [HttpPut("{id}")]
    public async Task<Results<NotFound, NoContent, Conflict<string>>> UpdateWallet(string id, [FromBody] WalletDto request)
    {
        var userId = GetUserId();

        var wallet = await _dbContext.Wallets
            .FirstOrDefaultAsync(e => 
                e.Id == id && 
                e.UserId == userId);

        if (wallet is null)
        {
            return TypedResults.NotFound();
        }

        if (await _dbContext.Wallets.AnyAsync(e => e.Name == request.Name && e.UserId == userId))
        {
            return TypedResults.Conflict("There is already a wallet with this name");
        }

		_mapper.Map(request, wallet);

        if (request.Disabled is not null)
        {
            wallet.Disabled = request.Disabled.Value;
        }

        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [HttpPost]
    public async Task<Results<Conflict<string>,Ok<Wallet>>> AddWallet([FromBody] WalletDto request)
    {
        var userId = GetUserId();

        if (await _dbContext.Wallets.AnyAsync(e => e.Name == request.Name && e.UserId == userId))
        {
            return TypedResults.Conflict("There is already a wallet with this name");
        }

        var wallet = new Wallet
        {
            Disabled = false,
            UserId = userId
        };

		_mapper.Map(request, wallet);

		_dbContext.Wallets.Add(wallet);
        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(wallet);
    }

    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> DeleteWallet(string id)
    {
        var userId = GetUserId();

        var wallet = await _dbContext.Wallets
            .FirstOrDefaultAsync(e => 
                e.Id == id && 
                e.UserId == userId);

        if (wallet is null)
        {
            return TypedResults.NotFound();
        }

		_dbContext.Wallets.Remove(wallet);
        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}