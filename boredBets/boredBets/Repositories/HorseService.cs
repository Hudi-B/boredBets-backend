using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
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

        public async Task<object> DeleteHorseAndJockeyByHorseId(Guid Id)
        {
            var horse = await _context.Horses
                .Include(h => h.Participants)
                .Include(j => j.Jockey)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);

            var jockey = await _context.Jockeys.FirstOrDefaultAsync(x => x.Id == horse.JockeyId);


            var searchInUserBet = await _context.UserBets.FirstOrDefaultAsync(x => x.HorseId == Id);

            if (horse == null)
            {
                return null;
            }

            var participants = horse?.Participants.ToList();

            if (participants != null && participants.Any())
            {
                _context.Participants.RemoveRange(participants);
                await _context.SaveChangesAsync(); // Save changes for the first deletion

                // Detach the entities to prevent further tracking issues
                foreach (var participant in participants)
                {
                    _context.Entry(participant).State = EntityState.Detached;
                }
            }

            // Detach the horse entity before making changes
            _context.Entry(horse).State = EntityState.Detached;

            // Load the horse again after the first deletion
            horse = await _context.Horses
                .Include(h => h.Participants)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (horse != null)
            {
                _context.Horses.Remove(horse);
                _context.Jockeys.Remove(jockey);
                await _context.SaveChangesAsync();
            }

            var result = new
            {
                Horse = horse,
                UserBet = searchInUserBet,
            };

            return result;
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
