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
        private readonly BoredbetsContext _context;

        public UserService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetByEmail(string Email)//tbc check if private
        {
            try
            {
                var racecountries = await _context.Users
                    .Where(u => u.Email == Email)
                    .ToListAsync();


                return racecountries;
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

        public async Task<User> Post(UserCreateDto userCreateDto)
        {
            try
            {
                var emailexist = await _context.Users.FirstOrDefaultAsync(x => x.Email == userCreateDto.Email);

                if (emailexist != null)
                {
                    throw new InvalidOperationException("User with this email already exists");
                }

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = userCreateDto.Email,
                    Role = "Member",
                    Password = userCreateDto.Password,
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

    }
}
