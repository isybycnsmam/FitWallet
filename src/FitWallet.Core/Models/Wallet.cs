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
    [StringLength(20, MinimumLength = 3)]
    public string Name { get; set; }

    public string Description { get; set; }

    ///// <summary>
    ///// Current balance, updated
    ///// </summary>
    //public double Balance { get; set; }// TODO: Implement that

    public bool Disabled { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public List<Transaction> Transactions { get; set; }
}
