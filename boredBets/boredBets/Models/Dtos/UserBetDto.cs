namespace boredBets.Models.Dtos
{
    public record UserBetCreateDto(Guid UserId, Guid First, Guid Second, Guid Third, Guid Fourth, Guid Fifth, Guid RaceId, int BetAmount,int BetTypeId);
}
