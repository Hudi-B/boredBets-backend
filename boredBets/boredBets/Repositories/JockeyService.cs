using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class JockeyService : IJockeyInterface
    {
        private readonly BoredbetsContext _context;

        public JockeyService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Jockey>> GetAllJockey()
        {
            return await _context.Jockeys
                                        .Include(h => h.Horses)
                                        .ToListAsync();
        }

        public async Task<Jockey> GetJockeyById(Guid JockeyId)
        {
            var jockey = await _context.Jockeys.FirstOrDefaultAsync(x => x.Id == JockeyId);

            if (jockey == null)
            {
                return null;
            }

            return jockey;
        }

        public async Task<object> GetJockeyDetailByJockeyId(Guid JockeyId)
        {
            var jockey = await _context.Jockeys.FirstOrDefaultAsync(x => x.Id == JockeyId);

            if (jockey == null)
            {
                return "0";
            }

            var jockeyHasHorse = await _context.Horses.FirstOrDefaultAsync(x => x.JockeyId == jockey.Id);

            if (jockeyHasHorse == null)
            {
                var jockeyWithoutHorse = new
                {
                    Id = jockey.Id,
                    Name = jockey.Name,
                    Country = jockey.Country,
                    Age = jockey.Age,
                    IsMale = jockey.Male,
                    HorseId = (Guid?)null,
                    HorseName = (string)null,
                    Next3Races = new List<Race>(), // Initialize with empty list
                    Past3Races = new List<Race>(), // Initialize with empty list
                    LifeTimeMatches = 0
                };
                return jockeyWithoutHorse;
            }

            var jockeyParticipate = await _context.Participants
                .Where(p => p.HorseId == jockeyHasHorse.Id)
                .Select(p => p.RaceId)
                .ToListAsync();

            var raceSchedulesPast = await _context.Races
                .Where(x => jockeyParticipate.Contains(x.Id) && x.RaceScheduled < DateTime.UtcNow)
                .ToListAsync();

            var raceSchedulesFuture = await _context.Races
                .Where(x => jockeyParticipate.Contains(x.Id) && x.RaceScheduled > DateTime.UtcNow)
                .ToListAsync();

            var result = new
            {
                Id = jockey.Id,
                Name = jockey.Name,
                Country = jockey.Country,
                Age = jockey.Age,
                IsMale = jockey.Male,
                HorseId = jockeyHasHorse.Id,
                HorseName = jockeyHasHorse.Name,
                Next3Races = raceSchedulesFuture.Take(3).ToList(), // Convert to list
                Past3Races = raceSchedulesPast.Take(3).ToList(), // Convert to list
                LifeTimeMatches = jockeyParticipate.Count
            };

            return result;
        }

        public async Task<Jockey> Post(JockeyCreateDto jockeyCreateDto)
        {
            var jockeys = new Jockey
            {
                Id = Guid.NewGuid(),
                Name = jockeyCreateDto.Name,
                Quality = jockeyCreateDto.Quality,
                Male = jockeyCreateDto.Male,
            };
            await _context.Jockeys.AddAsync(jockeys);
            await _context.SaveChangesAsync();

            return jockeys;
        }

    }
}
