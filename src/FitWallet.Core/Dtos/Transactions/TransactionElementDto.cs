namespace FitWallet.Core.Dtos.Transactions;

public class TransactionElementDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public int Quantity { get; set; }

    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
}