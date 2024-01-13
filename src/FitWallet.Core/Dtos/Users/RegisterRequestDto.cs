using System.ComponentModel.DataAnnotations;

namespace FitWallet.Core.Dtos.Users;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "User Name is required")]
    [StringLength(DtoValidationConsts.UserNameMinLength, MinimumLength = DtoValidationConsts.UserNameMinLength)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [StringLength(DtoValidationConsts.UserFirstNameMaxLength, MinimumLength = DtoValidationConsts.UserFirstNameMinLength)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(DtoValidationConsts.UserPasswordMaxLength, MinimumLength = DtoValidationConsts.UserPasswordMinLength)]
    public string Password { get; set; }
}