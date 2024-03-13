using System.Text;

namespace boredBets.Models.Dtos
{
    public record TrackContentDto(Guid Id, string Name, 
        string Country,float Length, string Address);
    public record TrackUpdateDto(string Name, string Country,
        float Length, string Address);

    public record TrackCreateDto(string Name, string Country,
        float Length, string Address);
}
