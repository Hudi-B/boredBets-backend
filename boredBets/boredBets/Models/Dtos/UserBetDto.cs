namespace boredBets.Models.Dtos
{
    public record UserBetCreateDto(Guid UserId, Guid HorseId, Guid RaceId, float BetAmount);
}
