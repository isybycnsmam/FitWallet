namespace FitWallet.Core;

public static class DtoValidationConsts
{
    #region User
    
    public const int UserNameMinLength = 3;
    public const int UserNameMaxLength = 25;

    public const int UserFirstNameMinLength = 3;
    public const int UserFirstNameMaxLength = 15;

    public const int UserPasswordMinLength = 8;
    public const int UserPasswordMaxLength = 30;

    #endregion

    #region Wallet
    
    public const int WalletNameMinLength = 3;
    public const int WalletNameMaxLength = 20;

    public const int WalletDescriptionMinLength = 0;
    public const int WalletDescriptionMaxLength = 300;

    #endregion
}