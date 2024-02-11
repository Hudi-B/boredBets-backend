namespace boredBets.Models.Dtos
{
    public record JockeyContentDto(Guid Id, string Name, int Quality);
    public record JockeyUpdateDto(string Name, int Quality);

    public record JockeyCreateDto(string Name, int Quality);
}
