namespace FitWallet.Core.Dtos.Transactions;

public class TransactionDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public DateTime OperationDate { get; set; }

    public string WalletId { get; set; }
    public string WalletName { get; set; }

    public string CompanyName { get; set; }
    public List<TransactionElementDto> Elements { get; set; }
}