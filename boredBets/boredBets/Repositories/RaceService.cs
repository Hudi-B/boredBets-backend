using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.AspNetCore.Mvc.Routing;
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
            DateTime now = DateTime.UtcNow;

            var allFutureRaces = await _context.Races
                .Include(r => r.Track)
                .Where(r => r.RaceScheduled > now)
                .OrderBy(r => r.RaceScheduled)
                .Select(r => new AllHappendRaceViewModel
                {
                    Id = r.Id,
                    RaceScheduled = r.RaceScheduled,
                    Name = r.Track.Name ,
                    Country = r.Track.Country ,
                })
                .ToListAsync();

            var OrderedList = allFutureRaces.OrderByDescending(r => r.RaceScheduled);

            return OrderedList.Reverse();
        }
        public async Task<IEnumerable<FiveRaceViewModel>> GetFutureRaces()
        {
            DateTime now = DateTime.UtcNow;

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
                })
                .ToListAsync();

            var OrderedList = futureRaces.OrderByDescending (r => r.RaceScheduled);

            return OrderedList.Reverse();
        }

        public async Task<IEnumerable<AllHappendRaceViewModel>> GetAllHappendRaces()
        {
            DateTime now = DateTime.UtcNow;

            var allHappenedRaces = await _context.Races
                .Include(r => r.Track)
                .Where(r => r.RaceScheduled < now)
                .Select(r => new AllHappendRaceViewModel
                {
                    Id = r.Id,
                    RaceScheduled = r.RaceScheduled,
                    Name = r.Track.Name,
                    Country = r.Track.Country,
                })
                .ToListAsync();

            return allHappenedRaces.OrderByDescending(r => r.RaceScheduled);
        }

        public async Task<IEnumerable<FiveRaceViewModel>> GetAlreadyHappenedRaces()
        {
            DateTime now = DateTime.UtcNow;

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
                })
                .ToListAsync();

            return alreadyHappenedRaces.OrderByDescending(r => r.RaceScheduled);
        }
        

        

        public async Task<object> Post(RaceCreateDto raceCreateDto)
        {
            var track = await _context.Tracks.FirstOrDefaultAsync(x => x.Id == raceCreateDto.TrackId);

            if (track == null)
            {
                return "0";
            }

            var race = new Race
            {
                Id = Guid.NewGuid(),
                Rain = raceCreateDto.Rain,
                TrackId = raceCreateDto.TrackId,
                RaceTime = raceCreateDto.RaceTime,
                RaceScheduled = raceCreateDto.RaceScheduled,

            };

            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();

            return race;
        }

        public async Task<object> GetByRaceId(Guid RaceId)
        {
            var race = await _context.Races
                                     .Include(x => x.Track)
                                     .SingleOrDefaultAsync(x => x.Id == RaceId);

            if (race == null)
            {
                return "0";
            }

            var participants = await _context.Participants
                                        .Where(p => p.RaceId == RaceId)
                                        .Join(_context.Horses,
                                              participant => participant.HorseId,
                                              horse => horse.Id,
                                              (participant, horse) => new
                                              {
                                                  HorseId = horse.Id,
                                                  HorseName = horse.Name,
                                                  HorseAge = horse.Age,
                                                  HorseCountry = horse.Country,
                                                  HorseStallion = horse.Stallion,
                                                  JockeyId = horse.JockeyId,
                                                  JockeyName = horse.Jockey.Name,
                                                  JockeyQuality = horse.Jockey.Quality,
                                                  JockeyCountry = horse.Jockey.Country,
                                                  JockeyMale = horse.Jockey.Male,
                                                  JockeyAge = horse.Jockey.Age
                                              })
                                        .ToListAsync();

            var result = new
            {
                RaceId = RaceId,
                RaceTime = race.RaceTime,
                RaceScheduled = race.RaceScheduled,
                Rain = race.Rain,
                Track = race.Track,
                Participants = participants
            };

            return result;
        }


        public async Task<IEnumerable<GetByCountryViewModel>> GetByCountry(string country)
        {
             var racecountries = await _context.Tracks
                    .Include(t => t.Races)
                    .Where(t => t.Country == country)
                    .SelectMany(t => t.Races.DefaultIfEmpty(), (t, race) => new GetByCountryViewModel
                    {
                        Id = race != null ? race.Id : Guid.Empty,
                        Country = t.Country,
                        Length = race != null ? t.Length : 0,
                        Rain = race.Rain
                    })
                    .ToListAsync();

            return racecountries;
        }

        public async Task<object> DeleteRaceById(Guid Id)
        {
            var race = await _context.Races.FirstOrDefaultAsync(x => x.Id == Id);

            if (race == null)
            {
                return null; 
            }


            _context.Races.Remove(race);
            await _context.SaveChangesAsync();

            var result = new
            {
                race
            };

            return result;
        }

    }
}
