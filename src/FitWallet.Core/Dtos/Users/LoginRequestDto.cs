using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Dtos.Users;

public class LoginRequestDto
{
    [Required(ErrorMessage = "User Name is required")]
    [StringLength(DtoValidationConsts.UserNameMaxLength, MinimumLength = DtoValidationConsts.UserNameMinLength)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(DtoValidationConsts.UserPasswordMaxLength, MinimumLength = DtoValidationConsts.UserPasswordMinLength)]
    public string Password { get; set; }
}