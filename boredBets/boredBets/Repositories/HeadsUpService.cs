using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class HeadsUpService : IHeadsUpInterface
    {
        private readonly BoredbetsContext _context;

        public HeadsUpService(BoredbetsContext context)
        {
            _context = context;
        }
        public async Task simulateRace()
        {
            var racesToSimulate = await _context.Races
                .Where(x => x.RaceScheduled < DateTime.UtcNow && x.Participants.Any(y => y.Placement.HasValue))
                .Include(x => x.Participants)
                    .ThenInclude(p => p.Horse)
                        .ThenInclude(p => p.Jockey)
                .ToListAsync();
            foreach (var race in racesToSimulate)
            { 

            }
        }
    }
}
