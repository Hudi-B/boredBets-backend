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
            return await _context.Jockeys.ToListAsync();
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
            var jockey = await _context.Jockeys.FirstOrDefaultAsync(x=>x.Id == JockeyId);

            if (jockey == null)
            {
                return "0";
            }

            var jockeyHasHorse = await _context.Horses.FirstOrDefaultAsync(x => x.JockeyId == jockey.Id);

            if (jockeyHasHorse == null)
            {
                var jockeyWithoutHorse = new {
                    Id = jockey.Id,
                    Name = jockey.Name,
                    IsMale = jockey.Male,
                    HorseId = jockeyHasHorse.Id,
                    HorseName = jockeyHasHorse.Name,
                };
            }

            var jockeyParicipate = await _context.Participants.AnyAsync(x => x.HorseId == jockeyHasHorse.Id);

            if (jockeyParicipate == false) 
            {
                var jockeyNeverRaced = new
                {
                    Id = jockey.Id,
                    Name = jockey.Name,
                    IsMale = jockey.Male,
                    HorseId = jockeyHasHorse.Id,
                    HorseName = jockeyHasHorse.Name,
                };
                return jockeyNeverRaced;
            }

            var jockeyParticipate = await _context.Participants
                                      .Where(x => x.HorseId == jockeyHasHorse.Id)
                                      .Select(x => new { x.RaceId })
                                      .FirstOrDefaultAsync();

            var raceSchedulesPast = await _context.Races
                                                    .Where(x => x.RaceScheduled < DateTime.UtcNow)
                                                    .ToListAsync();

            var raceSchedulesFuture = await _context.Races
                                                    .Where(x => x.RaceScheduled > DateTime.UtcNow)
                                                    .ToListAsync();





            var result = new
            {
                Id = jockey.Id,
                Name = jockey.Name,
                IsMale = jockey.Male,
                HorseId = jockeyHasHorse.Id,
                HorseName = jockeyHasHorse.Name,
                Next3Races = raceSchedulesFuture.Take(3),
                Past3Races = raceSchedulesPast.Take(3),
                LifeTimeMatches = raceSchedulesPast.Count()

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
