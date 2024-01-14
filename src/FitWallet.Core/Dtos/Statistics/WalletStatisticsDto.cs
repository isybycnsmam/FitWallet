namespace FitWallet.Core.Dtos.Statistics;

public sealed class WalletStatisticsDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public double TotalSpendings { get; set; }

    public List<CategoryStatisticsDto> Categories { get; set; }
}