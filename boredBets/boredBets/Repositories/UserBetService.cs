using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var id = await _context.UserBets
                                            .Where(x => x.Id == Id)
                                            .ToListAsync();
                if (id == null) 
                {
                    throw new Exception("User doesn1t exists");
                }

                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }

        public async Task<IEnumerable<UserBet>> GetAllUserBetsByUserId(Guid UserId)
        {
            try
            {
                var userBet = await _context.UserBets
                                                     .Where(x => x.UserId == UserId)
                                                     .ToListAsync();
                if (userBet==null)
                {
                    throw new Exception("The user has no bets or User doesn't exist");
                }

                return userBet;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }

        public async Task<UserBet> Post(Guid UserId, Guid HorseId, Guid RaceId, UserBetCreateDto userBetCreateDto)
        {
            try
            {
                // Check if the User, Horse, and Race exist
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                var horse = await _context.Horses.FirstOrDefaultAsync(x => x.Id == HorseId);
                var race = await _context.Races.FirstOrDefaultAsync(x => x.Id == RaceId);

                if (user == null || horse == null || race == null)
                {
                    // Handle the case where the User, Horse, or Race does not exist.
                    throw new Exception("User, Horse, or Race not found.");
                }

                // Create a new UserBet
                Guid id = new Guid();
                var userbet = new UserBet
                {
                    Id = id,
                    UserId = UserId,
                    RaceId = RaceId,
                    HorseId = HorseId,
                    BetAmount = userBetCreateDto.BetAmount
                };

                // Add the UserBet to the context and save changes
                await _context.UserBets.AddAsync(userbet);
                await _context.SaveChangesAsync();

                return userbet;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }

    }
}
