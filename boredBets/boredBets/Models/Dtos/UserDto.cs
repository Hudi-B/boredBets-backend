namespace boredBets.Models.Dtos
{
    public record UserCreateDto(string Email, string Password);
    public record UserDto(Guid Id,string Email, string Password,bool? Admin);
}
