using AutoMapper;
using FitWallet.Core.Dtos;
using FitWallet.Core.Dtos.Transactions;
using FitWallet.Core.Models.Transactions;
using FitWallet.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitWallet.Api.Controllers;

[Authorize]
public class TransactionsController(
	ILogger<TransactionsController> logger,
	IMapper mapper,
	ApplicationDatabaseContext dbContext) : ApplicationControllerBase(logger, mapper, dbContext)
{
	private const int PAGE_SIZE = 10;

	[HttpGet]
	public async Task<Results<BadRequest, Ok<PagedDto<TransactionDto>>>> GetTransactions([FromQuery] int page = 0)
	{
		if (page < 0)
			return TypedResults.BadRequest();

		var userId = GetUserId();
		var skip = page * PAGE_SIZE;

		var transactions = await _dbContext.Transactions
			.AsNoTracking()
			.Where(e => e.Wallet.UserId == userId)
			.Include(e => e.Wallet)
			.Include(e => e.Company)
			.Include(e => e.Elements)
			.ThenInclude(e => e.Category)
			.Skip(skip)
			.Take(PAGE_SIZE + 1)
			.ToListAsync();

		var transactionDtos = transactions
			.Take(PAGE_SIZE)
			.Select(MapTransactionDto)
			.ToList();

		return TypedResults.Ok(new PagedDto<TransactionDto>()
		{
			Data = transactionDtos,
			PageSize = PAGE_SIZE,
			PageCount = transactionDtos.Count,
			PageIndex = page,
			IsNext = transactions.Count > transactionDtos.Count
		});
	}

	[HttpGet("{id}")]
	public async Task<Results<NotFound, Ok<TransactionDto>>> GetTransaction(string id)
	{
		var userId = GetUserId();

		var transaction = await _dbContext.Transactions
			.AsNoTracking()
			.Where(e => e.Wallet.UserId == userId && e.Id == id)
			.Include(e => e.Wallet)
			.Include(e => e.Company)
			.Include(e => e.Elements)
				.ThenInclude(e => e.Category)
			.FirstOrDefaultAsync();

		if (transaction is null)
			return TypedResults.NotFound();

		var transactionDto = MapTransactionDto(transaction);

		return TypedResults.Ok(transactionDto);
	}

	private static TransactionDto MapTransactionDto(Transaction transaction)
	{
		return new TransactionDto
		{
			Id = transaction.Id,
			Description = transaction.Description,
			OperationDate = transaction.OperationDate,

			WalletId = transaction.WalletId,
			WalletName = transaction.Wallet.Name,

			CompanyName = transaction.Company.Name,
			Elements = transaction.Elements.Select(MapTransactionElementDtos).ToList()
		};
	}

	private static TransactionElementDto MapTransactionElementDtos(TransactionElement element)
	{
		return new TransactionElementDto
		{
			Id = element.Id,
			Name = element.Name,
			Description = element.Description,
			Value = element.Value,
			Quantity = element.Quantity,
			CategoryName = element.Category.Name,
		};
	}
}