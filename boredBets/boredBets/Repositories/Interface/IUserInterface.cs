using boredBets.Models;
using boredBets.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<User> Register(UserCreateDto userCreateDto);
        Task<object> Login(string email, string password);
        Task<object> GetNewAccessToken(Guid id, string refreshtoken);
        Task<IEnumerable<User>> GetAllUser();
        Task<IEnumerable<User>> GetByUserId(Guid id);
        //Task<IEnumerable<User>> GetByRole(string Role);

    }
}
