﻿using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace boredBets.Repositories
{
    public class UserService : IUserInterface
    {
       
        private readonly string _jwtSecret = "d7z9ncS3TqH0QtdM9L2DghR4FXvJYlVujjbK8YrAhzU=";
        private readonly string _jwtRefreshSecret = "Wt5XtVFvBdC3NkP8EwYrQgM1JsK6DbqMnZwHfVrKwRt=";

        private readonly BoredbetsContext _context;
        private readonly IConfiguration _configuration;

        public UserService(BoredbetsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private string GenerateRefreshToken(string Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtRefreshSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("UserId", Id) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateAccessToken(string Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("UserId", Id) }),
                Expires = DateTime.UtcNow.AddMinutes(59),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password) 
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(5);
            return BCrypt.Net.BCrypt.HashPassword(password,salt);
        }

        private bool VerifyHashedPassword(string enteredPassword, string storedPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        }

        public async Task<IEnumerable<User>> GetByUserId(Guid UserId)
        {
            var users = await _context.Users
                .Where(u => u.Id == UserId)
                .Select(u => new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Admin = u.Admin,
                    Created = u.Created,

                    UserDetail = !u.UserDetail.IsPrivate
                        ? u.UserDetail
                        : new UserDetail { IsPrivate = u.UserDetail.IsPrivate }
                })
                .ToListAsync();

            if (users == null ) 
            {
                return null;
            }

            return users;
        }


        public async Task<User> Register(UserCreateDto userCreateDto) //register
        {
            var emailExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == userCreateDto.Email);
            var usernameExist = await _context.Users.FirstOrDefaultAsync(x => x.Username == userCreateDto.Username);

            if (emailExist != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            if (usernameExist != null)
            {
                throw new InvalidOperationException("User with this name already exists");
            }

            string hashedpassword = HashPassword(userCreateDto.Password);

            Guid userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Email = userCreateDto.Email,
                Username = userCreateDto.Username,
                Wallet = 0,
                Admin = false,
                Password = hashedpassword,
                Created = DateTime.UtcNow,
                RefreshToken = GenerateRefreshToken(userId.ToString())
            };

            if (user == null)
            {
                return null;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDetails = new UserDetail
            {
                UserId = user.Id,
                Fullname = "-",
                Address = "-",
                IsPrivate = true,
                BirthDate = null
            };

            await _context.UserDetails.AddAsync(userDetails);
            await _context.SaveChangesAsync();

            return user;//only id
        }

        public async Task<object> Login(UserLoginDto userLoginDto) //login
        {
            var user = await _context.Users
                                          .Include(x => x.UserCards)
                                          .Include(x => x.UserDetail)
                                          .Include(x => x.UserBets)
                                          .FirstOrDefaultAsync(x => x.Email == userLoginDto.EmailOrUsername || x.Username==userLoginDto.EmailOrUsername);

            if (user == null || !VerifyHashedPassword(userLoginDto.Password, user.Password))
            {
                return null;
            }

            var accessToken = GenerateAccessToken(user.Id.ToString());
            var refreshToken = user.RefreshToken;

            if (user.RefreshToken == null)
            {
                GenerateRefreshToken(user.Id.ToString());
            }

            var response = new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Wallet = user.Wallet,
                Admin=user.Admin,
            };

            return response;
        }

        

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var result = await _context.Users
                                 .Include(u => u.UserDetail)
                                 .Select(u => new User
                                 {
                                     Id = u.Id,
                                     Email = u.Email,
                                     Admin = u.Admin
                                 })
                                 .ToListAsync();
            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task<object> GetNewAccessToken(Guid UserId, string refreshtoken)
        {
            var right_token = await _context.Users
                                                  .FirstOrDefaultAsync(x => x.RefreshToken == refreshtoken && x.Id == UserId);

            if (right_token == null)
            {
                return null;
            }

            var new_accesstoken = GenerateAccessToken(right_token.Id.ToString());

            return new { AccessToken = new_accesstoken };
        }

        public async Task<User> DeleteUserById(Guid id)
        {
            var userid = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userid == null)
            {
                return null; 
            }

            _context.Users.Remove(userid);
            await _context.SaveChangesAsync();

            return userid;
        }

        public async Task<UserWalletDto> GetWalletByUserId(Guid UserId)
        {
            var wallet = await _context.Users
                                       .Where(u => u.Id == UserId)
                                       .Select(u => new UserWalletDto(
                                           u.Wallet))
                                       .FirstOrDefaultAsync();

            if (wallet == null)
            {
                return null;
            }

            return wallet;
        }

        public async Task<UserWalletDto> UpdateWalletByUserId(Guid UserId,UserWalletDto wallet)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

            if (user == null)
            {
                return null;
            }
            if (user.Wallet<wallet.Wallet)
            {
                user.Wallet = wallet.Wallet;

                await _context.SaveChangesAsync();

                return new UserWalletDto(user.Wallet);
            }
            return null;

            
        }

    }
}
