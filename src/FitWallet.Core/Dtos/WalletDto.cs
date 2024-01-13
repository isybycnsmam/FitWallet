using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Dtos;

public class WalletDto
{
    public string Id { get; set; }

    [Required]
    [StringLength(DtoValidationConsts.WalletNameMaxLength, MinimumLength = DtoValidationConsts.WalletNameMinLength)]
    public string Name { get; set; }
        
    [StringLength(DtoValidationConsts.WalletDescriptionMaxLength, MinimumLength = DtoValidationConsts.WalletDescriptionMinLength)]
    public string Description { get; set; }

    public bool? Disabled { get; set; }
}