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

        public async Task<UserCard> Post(Guid Id, UserCardCreateDto userCardCreateDto)
        {
            try
            {
                var userDetail = await _context.UserDetails.FirstAsync(x => x.UserId == Id);

                if (userDetail == null)
                {
                    return null;
                }

                var existingUserCard = await _context.UserCards.FirstAsync(x => x.CreditcardNum == userCardCreateDto.CreditcardNum);

                if (existingUserCard != null)
                {
                    throw new Exception();
                }

                var userCard = new UserCard
                {
                    CreditcardNum = userCardCreateDto.CreditcardNum,
                    Cvc = userCardCreateDto.Cvc,
                    ExpDate = userCardCreateDto.ExpDate,
                    CardName = userCardCreateDto.CardName,
                    UserId = userDetail.UserId 
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
