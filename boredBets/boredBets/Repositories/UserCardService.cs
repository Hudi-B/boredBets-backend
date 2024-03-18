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

        public async Task<UserCard> DeleteByCreditCardNum(string CreditCardNum)
        {
            var creditCardNum = await _context.UserCards.FirstOrDefaultAsync(x => x.CreditcardNum == CreditCardNum);

            if (creditCardNum == null) 
            {
                return null;
            }

            _context.UserCards.Remove(creditCardNum);
            await _context.SaveChangesAsync();

            return creditCardNum;
        }

        public async Task<IEnumerable<UserCard>> GetAllUserCardsByUserId(Guid UserId)
        {
            var userid = await _context.UserCards
                                                 .Where(x => x.UserId == UserId)
                                                 .ToListAsync();

            if (userid == null)
            {
                return null;
            }

            return userid;

        }

        public async Task<UserCard> Post(UserCardCreateDto userCardCreateDto)
        {
            var userDetail = await _context.Users.FirstOrDefaultAsync(x => x.Id == userCardCreateDto.UserId);

            var existingUserCard = await _context.UserCards.FirstOrDefaultAsync(x => x.CreditcardNum == userCardCreateDto.CreditcardNum);

            if (existingUserCard != null && userDetail == null)
            {
                return null;
            }

            var userCard = new UserCard
            {
                CreditcardNum = userCardCreateDto.CreditcardNum,
                Cvc = userCardCreateDto.Cvc,
                ExpMonth = userCardCreateDto.ExpMonth,
                ExpYear = userCardCreateDto.ExpYear,
                CardName = !string.IsNullOrEmpty(userCardCreateDto.CardName) ? userCardCreateDto.CardName : "CreditCard",
                CardHoldername = userCardCreateDto.CardHoldername,
                UserId = userCardCreateDto.UserId
            };

            _context.UserCards.Add(userCard);
            await _context.SaveChangesAsync();

            return userCard;
        }


    }
}
