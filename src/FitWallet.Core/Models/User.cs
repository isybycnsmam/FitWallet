using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models;

/// <summary>
/// Is used for identifying people inside this application
/// </summary>
public sealed class User : ModelBase
{
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string LastName { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string PasswordHash { get; set; }

    public string Description { get; set; }

    public List<Wallet> Wallets { get; set; }
}