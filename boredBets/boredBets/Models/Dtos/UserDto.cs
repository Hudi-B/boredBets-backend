namespace boredBets.Models.Dtos
{
    public record UserCreateDto(string Email,string Username, string Password);
    public record UserLoginDto(string EmailOrUsername, string Password);
    public record UserDto(Guid Id,string Email, string Password,string Username, decimal Wallet, bool? Admin);
    public record UserWalletDto(string CreditCard, decimal Wallet);

    public record UsernameDto(string Username);
    public record UserEmailDto(string Email);
    public record UserPasswordDto(string oldPassword, string newPassword);
    public record UserUpdatebyUserIdPassword(string Password);
}
