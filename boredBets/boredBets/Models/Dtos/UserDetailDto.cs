namespace boredBets.Models.Dtos
{
    public record UserDetailCreateDto(string Fullname, string Address, bool IsPrivate, string PhoneNum, DateOnly BirthDate);
    public record UserDetailUpdateDto(string Fullname, string Address,string PhoneNum, DateOnly BirthDate);
}
