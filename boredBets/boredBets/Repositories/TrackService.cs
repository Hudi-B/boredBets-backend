using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class TrackService : ITrackInterface
    {
        private readonly BoredbetsContext _context;

        public TrackService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<Track> Post(TrackCreateDto trackCreateDto)
        {
            var tracks = new Track
            {
                Id = Guid.NewGuid(),
                Name = trackCreateDto.Name,
                Country = trackCreateDto.Country,
                Length = trackCreateDto.Length,
            };
            await _context.Tracks.AddAsync(tracks);
            await _context.SaveChangesAsync();

            return tracks;
        }
        public async Task<IEnumerable<Track>> GetAllTrack()
        {
            return await _context.Tracks.ToListAsync();
        }
    }
}
