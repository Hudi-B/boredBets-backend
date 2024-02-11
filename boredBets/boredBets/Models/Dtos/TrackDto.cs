using System.Text;

namespace boredBets.Models.Dtos
{
    public record TrackContentDto(Guid Id, string Name, 
        string Country,float Length, string Surface, bool Oval);
    public record TrackUpdateDto(string Name, string Country,
        float Length, string Surface, bool Oval);

    public record TrackCreateDto(string Name, string Country,
        float Length, string Surface, bool Oval);
}
