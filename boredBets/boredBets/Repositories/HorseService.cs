using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace boredBets.Repositories
{
    public class HorseService : IHorseInterface
    {
        private readonly IJockeyInterface jockeyInterface;

        private readonly BoredbetsContext _context;

        public HorseService(IJockeyInterface jockeyInterface, BoredbetsContext context)
        {
            this.jockeyInterface = jockeyInterface;
            _context = context;
        }

        public async Task<string> DeleteHorseById(Guid Id)
        {
            var horse = await _context.Horses.FirstOrDefaultAsync(x => x.Id == Id);

            if (horse == null)
            {
                return "0";
            }

            var jockey = await _context.Jockeys.FirstOrDefaultAsync(x => x.Id == horse.JockeyId);

            if (jockey == null)
            {
                return "1";
            }

            var participantToDelete = await _context.Participants.FirstOrDefaultAsync(x => x.HorseId == horse.Id);

            if (participantToDelete != null)
            {
                _context.Participants.Remove(participantToDelete);
            }
            await _context.SaveChangesAsync();//To make the trigger work

            var userBetsToDelete = await _context.UserBets
                .Where(x => x.First == Id || x.Second == Id || x.Third == Id || x.Fourth == Id || x.Fifth == Id)
                .ToListAsync();

            _context.UserBets.RemoveRange(userBetsToDelete);

            _context.Horses.Remove(horse);

            await _context.SaveChangesAsync();

            return "Success";
        }

        public async Task<IEnumerable<HorseContentDto>> GetAllHorse()
        {
            return await _context.Horses
                .Select(h => new HorseContentDto(
                    h.Id,
                    h.Name,
                    h.Age,
                    h.Stallion))
                .ToListAsync();
        }

        public async Task<HorseContentDto> GetHorseById(Guid HorseId)
        {
            var horse = await _context.Horses
                                            .Where(h => h.Id == HorseId)
                                            .Select(h => new HorseContentDto(
                                                h.Id,
                                                h.Name,
                                                h.Age,
                                                h.Stallion))
                                            .FirstOrDefaultAsync();

            if (horse == null)
            {
                return null;
            }

            return horse;
        }

        public async Task<object> GetHorseDetailByHorseId(Guid HorseId)
        {
            var horse = await _context.Horses.Include(x => x.Jockey).FirstOrDefaultAsync(x => x.Id == HorseId);

            if (horse == null)
            {
                return "0";
            }

            var horseParticipate = await _context.Participants.AnyAsync(x => x.HorseId == horse.Id);

            var horseParticipations = await _context.Participants
                .Where(x => x.HorseId == horse.Id && x.Placement != 0)
                .Select(x => x.Placement)
                .ToListAsync();

            List<Race> raceSchedulesPast = null;
            List<Race> raceSchedulesFuture = null;

            if (horseParticipate)
            {
                raceSchedulesPast = await _context.Races
                    .Where(x => x.RaceScheduled < DateTime.UtcNow && x.Participants.Any(x => x.HorseId == horse.Id))
                    .Include(x => x.Track)
                    .OrderBy(x => x.RaceScheduled)
                    .Take(3)
                    .ToListAsync();

                raceSchedulesFuture = await _context.Races
                    .Where(x => x.RaceScheduled > DateTime.UtcNow && x.Participants.Any(x => x.HorseId == horse.Id))
                    .Include(x => x.Track)
                    .OrderByDescending(x => x.RaceScheduled)
                    .Take(3)
                    .ToListAsync();
            }

            double avgPlacement = horseParticipations.Any() ? horseParticipations.Average() : 0;

            var result = new
            {
                Id = horse.Id,
                Name = horse.Name,
                Stallion = horse.Stallion,
                Country = horse.Country,
                Age = horse.Age,
                JockeyId = horse.Jockey.Id,
                JockeyName = horse.Jockey.Name,
                Next3Races = raceSchedulesFuture,
                Past3Races = raceSchedulesPast,
                RaceParticipatedIn = horseParticipations.Count(),
                AvgPlacement = avgPlacement,
            };

            return result;
        }


        public async Task<Horse> Post(HorseCreateDto horseCreateDto)
        {
            var horses = new Horse
            {
                Id = Guid.NewGuid(),
                Name = horseCreateDto.Name,
                Age = horseCreateDto.Age,
                Stallion = horseCreateDto.Stallion,
            };
            await _context.Horses.AddAsync(horses);
            await _context.SaveChangesAsync();

            return horses;
        }
        
        public async Task<bool> GenerateHorse(int quantity)
        {

            var freeJockeys =
                from jockey in _context.Jockeys
                where !_context.Horses.Any(horse => horse.JockeyId == jockey.Id)
                select jockey.Id; //selects free jockey that are not connected to any horse

            bool refreshList = false;

            if (freeJockeys.Count() < quantity)
            {
                refreshList = await jockeyInterface.GenerateJockey(quantity);
                if (!refreshList)
                {
                    return false;
                }
            }
            if (refreshList)
            {
                freeJockeys =
                    from jockey in _context.Jockeys
                    where !_context.Horses.Any(horse => horse.JockeyId == jockey.Id)
                    select jockey.Id;
            }

            List<string> maleHorseName = new List<string>();
            List<string> femaleHorseName = new List<string>();
            List<string> Countries = new List<string>();
            List<string> horseFirst = new List<string>();
            List<string> horseSecond = new List<string>();

            #region ReadFile

            string staticData = AppDomain.CurrentDomain.BaseDirectory.ToString() + "../../../staticData/";

            StreamReader sr;
            sr = new StreamReader(staticData + "maleHorses.txt");
            while (!sr.EndOfStream)
            {
                maleHorseName.Add(sr.ReadLine());
            }
            sr = new StreamReader(staticData + "femaleHorses.txt");
            while (!sr.EndOfStream)
            {
                femaleHorseName.Add(sr.ReadLine());
            }
            sr = new StreamReader(staticData + "countries.txt");
            while (!sr.EndOfStream)
            {
                Countries.Add(sr.ReadLine());
            }
            sr = new StreamReader(staticData + "horseFirst.txt");
            while (!sr.EndOfStream)
            {
                horseFirst.Add(sr.ReadLine());
            }
            sr = new StreamReader(staticData + "horseSecond.txt");
            while (!sr.EndOfStream)
            {
                horseSecond.Add(sr.ReadLine());
            }
            sr.Close();
            #endregion

            Random random = new Random();
            int maleHorseNameCount = maleHorseName.Count();
            int femaleHorseNameCount = femaleHorseName.Count();
            int countriesCount = Countries.Count();
            int horseFirstCount = horseFirst.Count();
            int horseSecondCount = horseSecond.Count();

            var freeJockeyIds = freeJockeys.ToList();

            for (int i = 0; i < quantity; i++)
            {
                bool male = random.Next(2) == 0;
                string name="";

                if (random.Next(10) != 0) // 1 out of 10 will not get a "first name"
                {
                    string originalString = horseFirst[random.Next(horseFirstCount)];
                    string capitalizedString = char.ToUpper(originalString[0]) + originalString.Substring(1);

                    name += capitalizedString + " ";
                }
                if (random.Next(4) != 0) //3 out of 4 will get an object as its second name
                {
                    name += horseSecond[random.Next(horseSecondCount)];
                }
                else 
                {
                    if (male)
                    {
                        name += maleHorseName[random.Next(maleHorseNameCount)];
                    }
                    else
                    {
                        name += femaleHorseName[random.Next(femaleHorseNameCount)];
                    }
                }
                var newHorse = new Horse
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Age = random.Next(5) + 2,
                    Country = Countries[random.Next(countriesCount)],
                    Stallion = male,
                    JockeyId = freeJockeyIds[i]
                };

                await _context.Horses.AddAsync(newHorse);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        
    }
}
