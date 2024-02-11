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
                var userDetail = await _context.UserDetails.FirstAsync(x => x.UserId == Id);

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

                    await _context.UserDetails.AddAsync(userDetail);
                }
                else
                {
                    userDetail.Fullname = userDetailCreateDto.Fullname;
                    userDetail.Address = userDetailCreateDto.Address;
                    userDetail.IsPrivate = userDetailCreateDto.IsPrivate;
                    userDetail.BirthDate = userDetailCreateDto.BirthDate;
                }

                await _context.SaveChangesAsync();
                return userDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }

    }
}
