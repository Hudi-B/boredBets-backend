namespace boredBets.Models.Dtos
{
    public record UserDetailCreateDto(string Fullname, string Address, bool IsPrivate, DateTime BirthDate);
    public record UserDetailUpdateDto(string Fullname, string Address, bool IsPrivate, DateTime BirthDate);
}
