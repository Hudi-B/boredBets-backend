namespace boredBets.Models.Dtos
{
    public record RaceCreateDto(double RaceTime,DateTime RaceScheduled, string Weather); 
    public record RaceDto(Guid Id, DateTime RaceTime);
}
