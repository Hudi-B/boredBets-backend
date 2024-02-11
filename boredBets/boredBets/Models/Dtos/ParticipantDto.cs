namespace boredBets.Models.Dtos
{
   public record ParticipantDto(Guid RaceId, Guid HorseId, Guid JockeyId, int Placement);
   public record ParticipantCreateDto(int Placement);
}
