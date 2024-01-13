using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models.Transactions;

/// <summary>
/// Describes single money operation, eg. payment or income
/// </summary>
public sealed class Transaction : ModelBase
{
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Name { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// If true this transaction should be excluded from balance calculations
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Date and time when this transaction happened
    /// </summary>
    [Required]
    public DateTime OperationDate { get; set; }

    /// <summary>
    /// Elements that are included in this transaction, eg. multi sport card, or pay
    /// </summary>
    public List<TransactionElement> Elements { get; set; }

    public string CompanyId { get; set; }
    public Company Company { get; set; }

    [Required]
    public string WalletId { get; set; }
    public Wallet Wallet { get; set; }
}
