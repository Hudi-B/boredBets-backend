using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;

namespace boredBets.Repositories
{
    public class UserService : IUserInterface
    {
        private readonly BoredbetsContext _context;

        public UserService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<User> Post(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = userCreateDto.Email,
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
    }
}
