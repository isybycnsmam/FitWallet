using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models;

/// <summary>
/// Is used for identifying people inside this application
/// </summary>
public class User : IdentityUser
{
    [Required]
    [StringLength(DtoValidationConsts.UserFirstNameMaxLength)]
    public string FirstName { get; set; }

    public List<Wallet> Wallets { get; set; }

    public List<Company> Companies { get; set; }

    public List<Category> Categories { get; set; }
}