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

        public async Task<UserBet> Post(UserBetCreateDto userBetCreateDto)
        {
            var existingParticipant = await _context.Participants.FirstOrDefaultAsync(x =>
                                                                                            x.RaceId == userBetCreateDto.RaceId &&
                                                                                            (x.HorseId == userBetCreateDto.First ||
                                                                                             x.HorseId == userBetCreateDto.Second ||
                                                                                             x.HorseId == userBetCreateDto.Third ||
                                                                                             x.HorseId == userBetCreateDto.Fourth ||
                                                                                             x.HorseId == userBetCreateDto.Fifth));



            var userCard = await _context.UserCards.FirstOrDefaultAsync(x => x.UserId == userBetCreateDto.UserId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.UserId);
            var race = await _context.Races.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.RaceId);


            if (user == null ||race == null || existingParticipant == null || userCard.CreditcardNum == null)
            {
                return null;
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

            int totalDeposit = await _context.UserBets
                                           .Where(x => x.UserId == userBetCreateDto.UserId)
                                           .SumAsync(x => x.BetAmount);

            var transaction = new Transaction 
            {
                UserId = userBetCreateDto.UserId,
                Deposit = totalDeposit+userBetCreateDto.BetAmount,
                Bet = userBetCreateDto.BetAmount,
                //BetOutcome = how to 
                Created = DateTime.UtcNow,
                
            };

            await _context.Transactions.AddAsync(transaction);

            await _context.UserBets.AddAsync(userbet);
            await _context.SaveChangesAsync();

            return userbet;
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
