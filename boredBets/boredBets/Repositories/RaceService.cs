using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    

    public class RaceService : IRaceInterface
    {
        private readonly BoredbetsContext _context;

        public RaceService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Race>> Get()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<AllHappendRaceViewModel>> GetAllFutureRaces()
        {
            DateTime now = DateTime.Now;

            var allFutureRaces = await _context.Races
                .Include(r => r.Track)
                .Where(r => r.RaceScheduled > now)
                .OrderBy(r => r.RaceScheduled)
                .Select(r => new AllHappendRaceViewModel
                {
                    Id = r.Id,
                    RaceScheduled = r.RaceScheduled,
                    Name = r.Track.Name
                })
                .ToListAsync();

            return allFutureRaces;
        }
        public async Task<IEnumerable<FiveRaceViewModel>> GetFutureRaces()
        {
            DateTime now = DateTime.Now;

            var futureRaces = await _context.Races
                .Include(r => r.Track)
                .Where(r => r.RaceScheduled > now)
                .OrderBy(r => r.RaceScheduled)
                .Take(5)
                .Select(r => new FiveRaceViewModel
                {
                    Id = r.Id,
                    RaceScheduled = r.RaceScheduled,
                    Country = r.Track.Country,
                    Length = r.Track.Length,
                    Oval = r.Track.Oval
                })
                .ToListAsync();

            return futureRaces;
        }

        public async Task<IEnumerable<AllHappendRaceViewModel>> GetAllHappendRaces()
        {
            DateTime now = DateTime.Now;

            var allHappenedRaces = await _context.Races
                .Include(r => r.Track)
                .Where(r => r.RaceScheduled < now)
                .Select(r => new AllHappendRaceViewModel
                {
                    Id = r.Id,
                    RaceScheduled = r.RaceScheduled,
                    Name = r.Track.Name
                })
                .ToListAsync();

            return allHappenedRaces;
        }

        public async Task<IEnumerable<FiveRaceViewModel>> GetAlreadyHappenedRaces()
        {
            DateTime now = DateTime.Now;

            var alreadyHappenedRaces = await _context.Races
                .Include(r => r.Track)
                .Where(r => r.RaceScheduled < now)
                .OrderByDescending(r => r.RaceScheduled)
                .Take(5)
                .Select(r => new FiveRaceViewModel
                {
                    Id = r.Id,
                    RaceScheduled = r.RaceScheduled,
                    Country = r.Track.Country,
                    Length = r.Track.Length,
                    Oval = r.Track.Oval
                })
                .ToListAsync();

            return alreadyHappenedRaces;
        }

        

        public async Task<Race> Post(Guid TrackId, RaceCreateDto raceCreateDto)
        {
            try
            {
                var track = await _context.Tracks.FirstOrDefaultAsync(x => x.Id == TrackId);

                if (track == null)
                {
                    throw new InvalidOperationException("Track not found.");
                }


                var race = new Race
                {
                    Id = Guid.NewGuid(),
                    Weather = raceCreateDto.Weather,
                    TrackId = TrackId,
                    RaceTime = raceCreateDto.RaceTime,
                    RaceScheduled= raceCreateDto.RaceScheduled,
                    
                };

                await _context.Races.AddAsync(race);
                await _context.SaveChangesAsync();

                return race;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }

        public async Task<Race> GetByRaceId(Guid RaceId)
        {
            try
            {
                var race = await _context.Races.SingleOrDefaultAsync(x => x.Id.Equals(RaceId));
                if (race == null)
                {
                    throw new Exception();
                }
                return race;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }

        public async Task<IEnumerable<GetByCountryViewModel>> GetByCountry(string country)
        {
            try
            {
                var racecountries = await _context.Tracks
                    .Include(t => t.Races)
                    .Where(t => t.Country == country)
                    .SelectMany(t => t.Races.DefaultIfEmpty(), (t, race) => new GetByCountryViewModel
                    {
                        Id = race != null ? race.Id : Guid.Empty,
                        Country = t.Country,
                        Length = race != null ? t.Length : 0,
                        Weather = race.Weather,
                        Oval = t.Oval,
                    })
                    .ToListAsync();

                return racecountries;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while retrieving the entity.", e);
            }
        }




    }
}
