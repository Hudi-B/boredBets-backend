using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<User> Post(UserCreateDto userCreateDto);
    }
}
