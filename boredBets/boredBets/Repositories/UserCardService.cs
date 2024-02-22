using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class UserCardService : IUserCardInterface
    {
        private readonly BoredbetsContext _context;

        public UserCardService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCard>> GetAllUserCardsByUserId(Guid UserId)
        {
            try
            {
                var userid = await _context.UserCards
                                                 .Where(x => x.UserId == UserId)
                                                 .ToListAsync();

                if (userid == null) 
                {
                    throw new Exception("User doesn't have a card");
                }

                return userid;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                throw new Exception("An error occurred while saving the entity changes.", e);
            }

        }

        public async Task<UserCard> Post(Guid UserId, UserCardCreateDto userCardCreateDto)
        {
            try
            {
                var userDetail = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

                if (userDetail == null)
                {
                    throw new Exception("User doesn't exists");
                }

                var existingUserCard = await _context.UserCards.FirstOrDefaultAsync(x => x.CreditcardNum == userCardCreateDto.CreditcardNum);

                if (existingUserCard != null)
                {
                    throw new Exception();
                }

                var userCard = new UserCard
                {
                    CreditcardNum = userCardCreateDto.CreditcardNum,
                    Cvc = userCardCreateDto.Cvc,
                    ExpMonth = userCardCreateDto.ExpMonth,
                    ExpYear = userCardCreateDto.ExpYear,
                    CardName = userCardCreateDto.CardName,
                    UserId = userDetail.Id
                };

                _context.UserCards.Add(userCard);
                await _context.SaveChangesAsync();

                return userCard;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }


    }
}
