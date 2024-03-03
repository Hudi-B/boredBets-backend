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

        public async Task<UserDetail> GetUserDetailByUserId(Guid UserId)
        {
            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (userDetail == null) 
            { 
                return null; 
            }

            return userDetail;
        }

        public async Task<UserDetail> Post(Guid UserId, UserDetailCreateDto userDetailCreateDto)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

            if (userExists == null)
            {
                return null;
            }
            else
            {
                var userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == UserId);

                if (userDetail == null)
                {
                    userDetail = new UserDetail
                    {
                        UserId = Guid.NewGuid(),
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
        }


    }
}
