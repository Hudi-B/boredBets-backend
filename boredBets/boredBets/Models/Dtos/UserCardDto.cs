namespace boredBets.Models.Dtos
{
    public record UserCardCreateDto(string CreditcardNum, string Cvc, string ExpMonth, string ExpYear, string CardName);
}
