using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<User> Post(UserCreateDto userCreateDto);
        Task<User> Get(string email, string password);

        Task<IEnumerable<User>> GetByEmail(string Email);
    }
}
