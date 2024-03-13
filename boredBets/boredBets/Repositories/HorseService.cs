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

        public async Task<object> DeleteHorseAndJockeyBy(Guid Id)
        {
            var id = await _context.Horses.FirstOrDefaultAsync(x => x.Id == Id);

            var searchInUserBet = await _context.UserBets.FirstOrDefaultAsync(x => x.HorseId == Id);

            


            if (id == null && searchInUserBet == null) 
            {
                return null;
            }

            _context.Horses.Remove(id);
            _context.Participants.Remove(null);
            await _context.SaveChangesAsync();

            var result = new
            {
                Horse = id,
                UserBet = searchInUserBet,
            };

            return result;
        }

        public async Task<IEnumerable<Horse>> GetAllHorse()
        {
            return await _context.Horses.ToListAsync();
        }

        public async Task<Horse> GetHorseById(Guid HorseId)
        {
            var horse = await _context.Horses.FirstOrDefaultAsync(x=>x.Id==HorseId);

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
