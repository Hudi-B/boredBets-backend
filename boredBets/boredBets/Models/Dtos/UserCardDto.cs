namespace boredBets.Models.Dtos
{
    public record UserCardCreateDto(int CreditcardNum, int Cvc, string ExpDate, string CardName);
}
