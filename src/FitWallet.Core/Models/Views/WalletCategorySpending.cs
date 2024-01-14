namespace FitWallet.Core.Models.Views;

public sealed class WalletCategorySpending
{
    public string WalletId { get; set; }
    public string WalletName { get; set; }
    public string UserId { get; set; }
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? CategoryDisplayColor { get; set; }
    public double? TotalSpent { get; set; }
}