using boredBets.Models;
using boredBets.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<object> Register(UserCreateDto userCreateDto);
        Task<object> Login(UserLoginDto userLoginDto);
        Task<object> UserLoginByRefreshToken(string RefreshToken);
        Task<object> GetNewAccessToken(Guid UserId, string refreshtoken);
        Task<string> VerificationCodeCheck(Guid UserId, string VerificationCode);
        Task<IEnumerable<object>> GetAllUser();
        Task<object> UserSinglePage(Guid UserId);
        Task<object> UpdateUsernameByUserId(Guid UserId, UsernameDto username);
        Task<object> GetByUserId(Guid UserId);
        Task<object> DeleteUserById(Guid UserId);
        Task<object> GetWalletByUserId(Guid UserId);
        Task<object> UpdateWalletByUserId(Guid UserId,UserWalletDto wallet);
        Task<string> UpdateEmailByUserId(Guid UserId, UserEmailDto emailDto);
        Task<string> UpdatePasswordByUserId(Guid UserId, UserUpdatebyUserIdPassword passwordDto);
        Task<string> UpadatePasswordByOldPassword(Guid UserId, UserPasswordDto passwordDto);
        Task<string> UpdateImageByUserId(Guid UserId, ImageUpdateByUserId imageUpdate);
        Task<string> DeleteImageByUserId(Guid UserId, ImageUpdateByUserId imageUpdate);
        Task<string> ForgotYourPassword(string Email);
        Task<string> Bonus(Guid UserId);
    }
}
