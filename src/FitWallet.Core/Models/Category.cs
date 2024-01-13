using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Models;

/// <summary>
/// Categories of transactions: health, dog items, to return etc
/// </summary>
public sealed class Category : ModelBase
{
    [Required]
    public string Name { get; set; }

    [Range(0, 16777215)]
    public int DisplayColor { get; set; }

    public string ParentId { get; set; }
    public Category Parent { get; set; }
}
