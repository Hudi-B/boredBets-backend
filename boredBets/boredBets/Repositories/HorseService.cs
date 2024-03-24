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
        private readonly BoredbetsContext _context;

        public HorseService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteHorseAndJockeyByHorseId(Guid Id)
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

            var searchInUserBets = await _context.UserBets
                                                          .Where(x => x.HorseId == Id)
                                                          .ToListAsync();

            if (searchInUserBets != null) 
            {
                _context.UserBets.RemoveRange(searchInUserBets);
            }

            _context.Horses.Remove(horse);
            _context.Jockeys.Remove(jockey);
            _context.SaveChanges();

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
            var horse = await _context.Horses.FirstOrDefaultAsync(x => x.Id == HorseId);

            if (horse == null)
            {
                return "0";
            }
            var jockeyName = await _context.Jockeys
                                    .Where(j => j.Id == horse.JockeyId)
                                    .Select(j => j.Name)
                                    .FirstOrDefaultAsync();

            var horseParicipate = await _context.Participants.AnyAsync(x => x.HorseId == horse.Id);

            if (horseParicipate == false)
            {
                var horseNeverRaced = new
                {
                    Id = horse.Id,
                    Name = horse.Name,
                    Stallion = horse.Stallion,
                    Country= horse.Country,
                    Age = horse.Age,
                    JockeyId = horse.JockeyId,
                    JockeyName = jockeyName
                };
                return horseNeverRaced;
            }

            var raceSchedulesPast = await _context.Races
                                                    .Where(x => x.RaceScheduled < DateTime.UtcNow && horseParicipate)
                                                    .ToListAsync();

            var raceSchedulesFuture = await _context.Races
                                                    .Where(x => x.RaceScheduled > DateTime.UtcNow && horseParicipate)
                                                    .ToListAsync();

            var result = new
            {
                Id = horse.Id,
                Name = horse.Name,
                Stallion = horse.Stallion,
                Country = horse.Country,
                Age = horse.Age,
                JockeyId = horse.JockeyId,
                JockeyName = jockeyName,
                Next3Races = raceSchedulesFuture.Take(3),
                Past3Races = raceSchedulesPast.Take(3),
                LifeTimeMatches = raceSchedulesPast.Count()
                //AvaragePlacement = WIP
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
        
    }
}
