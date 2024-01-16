using AutoMapper;
using FitWallet.Core.Dtos.Statistics;
using FitWallet.Core.Models.Views;
using FitWallet.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitWallet.Api.Controllers
{
    [Authorize]
    public class StatisticsController : ApplicationControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public StatisticsController(
            ILogger<StatisticsController> logger,
            IMapper mapper,
            ApplicationDatabaseContext context) : base(logger, mapper)
        {
            _context = context;
        }

        [HttpGet("wallets")]
        public async Task<Ok<List<WalletStatisticsDto>>> GetWalletsStatistics()
        {
            var userId = GetUserId();

            var userWalletsCategoriesSpendings = await _context.WalletsSpendingsPerCategory
                .Where(e => e.UserId == userId)
                .ToListAsync();

            var statistics = userWalletsCategoriesSpendings
                .GroupBy(e => e.WalletId)
                .Select(MapWalletStatisticsDto)
                .ToList();

            return TypedResults.Ok(statistics);
        }

        private static WalletStatisticsDto MapWalletStatisticsDto(IGrouping<string, WalletCategorySpending> walletSpendings)
        {
            var firstItem = walletSpendings.First();
            var totalSpendings = walletSpendings.Sum(e => e.TotalSpent ?? 0);

            var categorySpendings = walletSpendings
                .Where(e => e.TotalSpent is not null and not 0)
                .Select(e => new CategoryStatisticsDto
                {
                    Id = e.CategoryId,
                    Name = e.CategoryName,
                    TotalSpendings = e.TotalSpent.Value,
                    DisplayColor = e.CategoryDisplayColor ?? 0
                })
                .OrderByDescending(e => e.TotalSpendings)
                .ToList();

            return new WalletStatisticsDto()
            {
                Id = walletSpendings.Key,
                Name = firstItem.WalletName,
                TotalSpendings = totalSpendings,
                Categories = categorySpendings
            };
        }
    }
}
