using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace boredBets.Repositories
{
    public class UserBetService : IUserBetInterface
    {
        private readonly BoredbetsContext _context;

        public UserBetService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserBet>> GetAllUserBetsById(Guid Id)
        {
            var id = await _context.UserBets
                                            .Where(x => x.Id == Id)
                                            .ToListAsync();
            if (id == null)
            {
                return null;
            }

            return id;
        }

        public async Task<IEnumerable<UserBet>> GetAllUserBetsByUserId(Guid UserId)
        {
            var userBet = await _context.UserBets
                                                     .Where(x => x.UserId == UserId)
                                                     .ToListAsync();
            if (userBet == null)
            {
                return null;
            }

            return userBet;
        }

        public async Task<UserBet> Post(UserBetCreateDto userBetCreateDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.UserId);
            var horse = await _context.Horses.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.HorseId);
            var race = await _context.Races.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.RaceId);

            if (user == null || horse == null || race == null)
            {
                return null;
            }

            Guid id = new Guid();
            var userbet = new UserBet
            {
                Id = id,
                UserId = userBetCreateDto.UserId,
                RaceId = userBetCreateDto.RaceId,
                HorseId = userBetCreateDto.HorseId,
                BetAmount = userBetCreateDto.BetAmount
            };

            await _context.UserBets.AddAsync(userbet);
            await _context.SaveChangesAsync();

            return userbet;
        }

    }
}
