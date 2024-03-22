namespace boredBets.Models.Dtos
{
    public record RaceCreateDto(int TrackId,double RaceTime,DateTime RaceScheduled, string Weather); 
    public record RaceDto(Guid Id, DateTime RaceTime);
    
}
