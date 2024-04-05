namespace boredBets.Models.Dtos
{
    public record RaceCreateDto(int TrackId,double RaceTime,DateTime RaceScheduled, bool Rain); 
    public record RaceDto(Guid Id, DateTime RaceTime);
    
}
