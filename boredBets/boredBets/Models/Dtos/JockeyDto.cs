namespace boredBets.Models.Dtos
{
    public record JockeyContentDto(Guid Id, string Name, int Quality, bool Male);
    public record JockeyUpdateDto(string Name, int Quality, bool Male);
    public record JockeyCreateDto(string Name, int Quality, bool Male);
}
