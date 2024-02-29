namespace boredBets.Models.Dtos
{
    public record UserCardCreateDto(Guid UserId,string CreditcardNum, string Cvc, string ExpMonth, string ExpYear, string CardName);
}
