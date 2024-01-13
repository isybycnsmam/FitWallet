using FitWallet.Core.Models.Transactions;
using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models;

/// <summary>
/// Holds information about single money storage for one person.<br/>
/// Eg. cash, bank account or sock.
/// </summary>
public sealed class Wallet : ModelBase
{
    [Required]
    [StringLength(DtoValidationConsts.WalletNameMaxLength)]
    public string Name { get; set; }
    
    [StringLength(DtoValidationConsts.WalletDescriptionMaxLength)]
    public string Description { get; set; }

    // TODO: Implement balance

    public bool Disabled { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }

    public List<Transaction> Transactions { get; set; }
}
