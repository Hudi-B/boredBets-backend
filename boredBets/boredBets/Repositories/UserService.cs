using boredBets.Models;
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

        public async Task<IEnumerable<User>> GetByUserId(Guid id)
        {
            var users = await _context.Users
                .Where(u => u.Id == id)
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
            var emailexist = await _context.Users.FirstOrDefaultAsync(x => x.Email == userCreateDto.Email);

            if (emailexist != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            string hashedpassword = HashPassword(userCreateDto.Password);

            Guid userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Email = userCreateDto.Email,
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

        public async Task<object> Login(UserCreateDto userCreateDto) //login
        {
            var user = await _context.Users
                                          .Include(x => x.UserCards)
                                          .Include(x => x.UserDetail)
                                          .Include(x => x.UserBets)
                                          .FirstOrDefaultAsync(x => x.Email == userCreateDto.Email);

            if (user == null || !VerifyHashedPassword(userCreateDto.Password, user.Password))
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
                Admin=user.Admin,
            };

            return response;
        }

        /*public async Task<IEnumerable<User>> GetByRole(string Role)
        {
            try
            {
                var userRole = await _context.Users
                                                    .Where(x => x.Role == Role)
                                                    .ToListAsync();
                if (userRole == null)
                {
                    new Exception("No user has this role or the role doesn't exist");
                }
                return userRole;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw;
            }
        }*/

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

        public async Task<object> GetNewAccessToken(Guid id, string refreshtoken)
        {
            var right_token = await _context.Users
                                                  .FirstOrDefaultAsync(x => x.RefreshToken == refreshtoken && x.Id == id);

            if (right_token == null)
            {
                return null;
            }

            var new_accesstoken = GenerateAccessToken(right_token.Id.ToString());

            return new { AccessToken = new_accesstoken };
        }

    }
}
