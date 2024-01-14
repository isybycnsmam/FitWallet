namespace FitWallet.Core.Dtos.Statistics;

public sealed class CategoryStatisticsDto
{
    public string Id { get; set;}
    public string Name { get; set;}
    public double TotalSpendings { get; set; }
    public int DisplayColor { get; set; }
}