using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace boredBets.Repositories
{
    public class UserService : IUserInterface
    {
        private string HashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyHashedPassword(string enteredPassword, string storedPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        }

        private readonly BoredbetsContext _context;

        public UserService(BoredbetsContext context)
        {
            _context = context;
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
                    Role = u.Role,
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


        public async Task<User> Post(UserCreateDto userCreateDto) //register
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
                    Role = "Member",
                    Password = hashedpassword,
                    Created = DateTime.UtcNow,
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

        public async Task<User> Get(string email, string password) //login
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


                return user;
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

        public async Task<IEnumerable<User>> GetByRole(string Role)
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
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users
                                        .Include(u => u.UserDetail)
                                        .ToListAsync();
        }
    }
}
