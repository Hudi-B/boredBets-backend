﻿using boredBets.Models;
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
        Task<IEnumerable<User>> GetAllUser();
        Task<object> GetUserDetailsByUserId(Guid UserId);
        Task<object> UpdateUsernameByUserId(Guid UserId, UsernameDto username);
        Task<object> GetByUserId(Guid UserId);
        Task<object> DeleteUserById(Guid UserId);
        Task<object> GetWalletByUserId(Guid UserId);
        Task<object> UpdateWalletByUserId(Guid UserId,UserWalletDto wallet);
        Task<string> UpdateEmailByUserId(Guid UserId, UserEmailDto emailDto);
        Task<string> UpdatePasswordByUserId(Guid UserId, UserPasswordDto passwordDto);
    }
}
