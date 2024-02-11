using boredBets.Models.Dtos;
using boredBets.Models;

namespace boredBets.Repositories.Interface
{
    public interface ITrackInterface
    {
        Task<Track> Post(TrackCreateDto trackCreateDto);
    }
}
