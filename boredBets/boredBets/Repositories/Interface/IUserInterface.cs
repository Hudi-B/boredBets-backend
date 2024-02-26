using boredBets.Models;
using boredBets.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<User> Register(UserCreateDto userCreateDto);
        Task<object> Login(string email, string password);
        Task<string> GetCheckRefreshToken();
        Task<IEnumerable<User>> GetAllUser();
        Task<IEnumerable<User>> GetByEmail(string Email);
        //Task<IEnumerable<User>> GetByRole(string Role);

    }
}
