namespace boredBets.Models.Dtos
{
    public record UserCreateDto(string Email,string Username, string Password);
    public record UserLoginDto(string EmailOrUsername, string Password);
    public record UserDto(Guid Id,string Email, string Password,string Username, int Wallet, bool? Admin);
    public record UserWalletDto(int Wallet);

    public record UsernameDto(string Username);
    public record UserEmailDto(string Email);
    public record UserPasswordDto(string Password);
}
