using System.Text;

namespace boredBets.Models.Dtos
{
    public record TrackContentDto(Guid Id, string Name, 
        string Country,float Length);
    public record TrackUpdateDto(string Name, string Country,
        float Length);

    public record TrackCreateDto(string Name, string Country,
        float Length);
}
