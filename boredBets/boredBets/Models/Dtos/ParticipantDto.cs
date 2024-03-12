namespace boredBets.Models.Dtos
{
   public record ParticipantDto(Guid RaceId, Guid HorseId,int Placement);
   public record ParticipantCreateDto(int Placement);
}
