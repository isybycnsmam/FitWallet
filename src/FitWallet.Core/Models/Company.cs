using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models;

/// <summary>
/// Represents subject with whom we made transaction
/// </summary>
public sealed class Company : ModelBase
{
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Name { get; set; }

    public string Address { get; set; }

    public string ParentId { get; set; }
    public Company Parent { get; set; }
}
