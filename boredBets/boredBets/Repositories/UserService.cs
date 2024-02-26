using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private string GenerateRefreshToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtRefreshSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("userEmail", email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateAccessToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("userEmail", email) }),
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

        public async Task<IEnumerable<User>> GetByEmail(string Email)
        {
            try
            {
                var users = await _context.Users
                .Where(u => u.Email == Email)
                .Select(u => new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Admin = u.Admin,
                    Created=u.Created,
                   
                    UserDetail = !u.UserDetail.IsPrivate
                        ? u.UserDetail
                        : new UserDetail { IsPrivate = u.UserDetail.IsPrivate }
                })
                .ToListAsync();


                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while retrieving the entity.", e);
            }
        }


        public async Task<User> Register(UserCreateDto userCreateDto) //register
        {
            try
            {
                var emailexist = await _context.Users.FirstOrDefaultAsync(x => x.Email == userCreateDto.Email);

                if (emailexist != null)
                {
                    throw new InvalidOperationException("User with this email already exists");
                }

                string hashedpassword = HashPassword(userCreateDto.Password);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = userCreateDto.Email,
                    Admin = false,
                    Password = hashedpassword,
                    Created = DateTime.UtcNow,
                    RefreshToken=GenerateRefreshToken(userCreateDto.Email)
                };

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
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw; 
            }
        }

        public async Task<object> Login(string email, string password) //login
        {
            try
            {
                var user = await _context.Users
                                          .Include(x => x.UserCards)
                                          .Include(x => x.UserDetail)
                                          .Include (x => x.UserBets)
                                          .FirstOrDefaultAsync(x => x.Email == email);
                                          
                if (user == null || !VerifyHashedPassword(password,user.Password))
                {
                    throw new Exception("Unauthorized"); 
                }

                var accessToken = GenerateAccessToken(user.Email);
                var refreshToken = user.RefreshToken;

                var response = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                };

                return response;
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
            return await _context.Users
                                        .Include(u => u.UserDetail)
                                        .ToListAsync();
        }

        public async Task<string> GetCheckRefreshToken(Guid id, string refreshtoken)
        {
            try
            {
                var right_token = await _context.Users
                                                  .FirstOrDefaultAsync(x => x.RefreshToken == refreshtoken && x.Id == id);

                if (right_token == null)
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }

                var new_accesstoken = GenerateAccessToken(right_token.Email); // Assuming there is an Email property in your User entity
                return new_accesstoken;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
