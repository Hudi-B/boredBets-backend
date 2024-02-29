using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IUserCardInterface
    {
        Task<UserCard> Post(UserCardCreateDto userCardCreateDto);
        Task<IEnumerable<UserCard>> GetAllUserCardsByUserId(Guid UserId);
    }
}
