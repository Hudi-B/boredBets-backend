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
