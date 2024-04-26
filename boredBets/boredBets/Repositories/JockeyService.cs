using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

            var Participated = await _context.Participants.AnyAsync(x => x.Horse.JockeyId == JockeyId);

            var Placements = await _context.Participants
                .Where(x => x.Horse.JockeyId == JockeyId && x.Placement != 0)
                .Select(x => x.Placement)
                .ToListAsync();

            List<Race> raceSchedulesPast = null;
            List<Race> raceSchedulesFuture = null;

            if (Participated)
            {
                raceSchedulesPast = await _context.Races
                    .Where(x => x.RaceScheduled < DateTime.UtcNow && x.Participants.Any(x => x.Horse.JockeyId == JockeyId))
                    .Include(x => x.Track)
                    .OrderBy(x => x.RaceScheduled)
                    .Take(3)
                    .ToListAsync();

                raceSchedulesFuture = await _context.Races
                    .Where(x => x.RaceScheduled > DateTime.UtcNow && x.Participants.Any(x => x.Horse.JockeyId == JockeyId))
                    .Include(x => x.Track)
                    .OrderByDescending(x => x.RaceScheduled)
                    .Take(3)
                    .ToListAsync();
            }

            var jockeysHorse = await _context.Horses.FirstOrDefaultAsync(x => x.JockeyId == jockey.Id);

            if (jockeysHorse == null)
            {
                var jockeyWithoutHorse = new
                {
                    Id = jockey.Id,
                    Name = jockey.Name,
                    Country = jockey.Country,
                    Age = jockey.Age,
                    IsMale = jockey.Male,
                    HorseId = (Guid?)null,
                    HorseName = (string?)null,
                    Next3Races = raceSchedulesFuture,
                    Past3Races = raceSchedulesPast,
                    RaceParticipatedIn = Placements.Count(),
                    AvgPlacement = Placements.Any() ? Placements.Average() : 0,
                };
                return jockeyWithoutHorse;
            }

            var result = new
            {
                Id = jockey.Id,
                Name = jockey.Name,
                Country = jockey.Country,
                Age = jockey.Age,
                IsMale = jockey.Male,
                HorseId = jockeysHorse.Id,
                HorseName = jockeysHorse.Name,
                Next3Races = raceSchedulesFuture,
                Past3Races = raceSchedulesPast,
                RaceParticipatedIn = Placements.Count(),
                AvgPlacement = Placements.Any() ? Placements.Average() : 0,
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

        public async Task<bool> GenerateJockey(int quantity)
        {
            try
            {
                List<string> familyNames = new List<string>();

                List<string> maleNames = new List<string>();
                List<string> femaleNames = new List<string>();

                List<string> maleMiddleNames = new List<string>();
                List<string> femaleMiddleNames = new List<string>();

                List<string> Countries = new List<string>();

                #region ReadFile
                string staticData = AppDomain.CurrentDomain.BaseDirectory.ToString() + "../../../staticData/";

                StreamReader sr;
                sr = new StreamReader(staticData + "familyNames.txt");
                while (!sr.EndOfStream)
                {
                    familyNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "maleNames.txt");
                while (!sr.EndOfStream)
                {
                    maleNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "femaleNames.txt");
                while (!sr.EndOfStream)
                {
                    femaleNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "maleMiddleNames.txt");
                while (!sr.EndOfStream)
                {
                    maleMiddleNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "femaleMiddleNames.txt");
                while (!sr.EndOfStream)
                {
                    femaleMiddleNames.Add(sr.ReadLine());
                }
                sr = new StreamReader(staticData + "countries.txt");
                while (!sr.EndOfStream)
                {
                    Countries.Add(sr.ReadLine());
                }
                sr.Close();
                #endregion

                Random random = new Random();
                for (int i = 0; i < quantity; i++)
                {
                    bool male = random.Next(2) == 0;
                    string name = familyNames[random.Next(familyNames.Count())];

                    if (male)
                    {
                        if (random.Next(10) == 6)
                        {
                            name += " " + maleMiddleNames[random.Next(maleMiddleNames.Count())];
                        }
                        name += " " + maleNames[random.Next(maleNames.Count())];
                    }
                    else
                    {
                        if (random.Next(10) == 6)
                        {
                            name += " " + femaleMiddleNames[random.Next(femaleMiddleNames.Count())];
                        }
                        name += " " + femaleNames[random.Next(femaleNames.Count())];
                    }

                    var newJockey = new Jockey
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Country = Countries[random.Next(Countries.Count())],
                        Age = random.Next(24, 38),
                        Quality = random.Next(10) + 1,
                        Male = male
                    };

                    await _context.Jockeys.AddAsync(newJockey);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
