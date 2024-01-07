using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models.Transactions;

/// <summary>
/// Single element on transaction, can be income or spending.
/// </summary>
public sealed class TransactionElement : ModelBase
{
    public double Value { get; set; }
    public string Description { get; set; }

    [Required]
    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
