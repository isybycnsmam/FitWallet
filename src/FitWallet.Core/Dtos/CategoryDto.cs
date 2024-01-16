using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Dtos;

public class CategoryDto
{
    public string Id { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 3)]
    public string Name { get; set; }

    [Range(1, 16777215)]
    public int DisplayColor { get; set; }
}