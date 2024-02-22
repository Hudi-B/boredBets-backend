using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class UserDetailService : IUserDetailInterface
    {
        private readonly BoredbetsContext _context;

        public UserDetailService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<UserDetail> Post(Guid Id, UserDetailCreateDto userDetailCreateDto)
{
    try
    {
        var userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == Id);

        if (userDetail == null)
        {
            userDetail = new UserDetail
            {
                UserId = Id,
                Fullname = userDetailCreateDto.Fullname,
                Address = userDetailCreateDto.Address,
                IsPrivate = userDetailCreateDto.IsPrivate,
                BirthDate = userDetailCreateDto.BirthDate,
            };

            // Add the new UserDetail entity to the context
            await _context.UserDetails.AddAsync(userDetail);
        }
        else
        {
            // Update existing UserDetail entity
            userDetail.Fullname = userDetailCreateDto.Fullname;
            userDetail.Address = userDetailCreateDto.Address;
            userDetail.IsPrivate = userDetailCreateDto.IsPrivate;
            userDetail.BirthDate = userDetailCreateDto.BirthDate;
        }

        // Save all changes in a single transaction
        await _context.SaveChangesAsync();

        return userDetail;
    }
    catch (Exception e)
    {
        // Log the error using a proper logging framework
        Console.WriteLine($"Error: {e.Message}");

        // Rethrow with a more general message
        throw new Exception("An error occurred while saving the entity changes.", e);
    }
}


    }
}
