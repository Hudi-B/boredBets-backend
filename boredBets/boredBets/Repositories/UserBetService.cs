using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<UserBet> GetUserBetsById(Guid Id)
        {
            var id = await _context.UserBets.FirstOrDefaultAsync(x => x.Id == Id);

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

        public async Task<object> Post(UserBetCreateDto userBetCreateDto)
        {
            var horses = new List<Guid> { userBetCreateDto.First, userBetCreateDto.Second, userBetCreateDto.Third, userBetCreateDto.Fourth, userBetCreateDto.Fifth };

            var missingParticipant = horses.FirstOrDefault(horseId =>
                !_context.Participants.Any(x => x.RaceId == userBetCreateDto.RaceId && x.HorseId == horseId));

            if (missingParticipant != Guid.Empty)
            {
                return "1";
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.UserId);
            var userBetted = await _context.UserBets.FirstOrDefaultAsync(u => u.UserId == user.Id && u.RaceId == userBetCreateDto.RaceId);
            var usercard = await _context.UserCards.AnyAsync(u => u.UserId == user.Id);
            var inTheFuture = await _context.Races.FirstOrDefaultAsync(x =>x.Id == userBetCreateDto.RaceId && x.RaceScheduled > DateTime.UtcNow.AddMinutes(5));

            if (user == null || inTheFuture == null || !usercard)
            {
                return "2";
            }

            if (userBetted != null)
            {
                return "0";
            }

            if (userBetCreateDto.BetAmount > user.Wallet)
            {
                return "3";
            }

            var userbet = new UserBet
            {
                Id = Guid.NewGuid(),
                UserId = userBetCreateDto.UserId,
                RaceId = userBetCreateDto.RaceId,
                First = userBetCreateDto.First,
                Second = userBetCreateDto.Second,
                Third = userBetCreateDto.Third,
                Fourth = userBetCreateDto.Fourth,
                Fifth = userBetCreateDto.Fifth,
                BetAmount = userBetCreateDto.BetAmount,
                BetTypeId = userBetCreateDto.BetTypeId,
            };


            var transaction = new Transaction 
            {
                UserId = userBetCreateDto.UserId,
                Deposit = userBetCreateDto.BetAmount,
                Created = DateTime.UtcNow,
                
            };

            await _context.Transactions.AddAsync(transaction);

            await _context.UserBets.AddAsync(userbet);
            await _context.SaveChangesAsync();

            return "Success";
        }

        public async Task<UserBet> DeleteUserBetById(Guid Id)
        {
            var id = await _context.UserBets.FirstOrDefaultAsync(x=>x.Id==Id);

            if (id == null)
            {
                return null;
            }

            _context.UserBets.Remove(id);
            await _context.SaveChangesAsync();
            
            return id;
        }
    }
}
