using boredBets.Models;
using boredBets.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<User> Register(UserCreateDto userCreateDto);
        Task<object> Login(UserLoginDto userLoginDto);
        Task<object> UserLoginByRefreshToken(string RefreshToken);
        Task<object> GetNewAccessToken(Guid UserId, string refreshtoken);
        Task<IEnumerable<User>> GetAllUser();
        Task<object> GetUserDetailsByUserId(Guid UserId);
        Task<object> UpdateUsernameByUserId(Guid UserId, UsernameDto username);
        Task<object> GetByUserId(Guid UserId);
        Task<User> DeleteUserById(Guid UserId);
        Task<UserWalletDto> GetWalletByUserId(Guid UserId);
        Task<UserWalletDto> UpdateWalletByUserId(Guid UserId,UserWalletDto wallet);
    }
}
