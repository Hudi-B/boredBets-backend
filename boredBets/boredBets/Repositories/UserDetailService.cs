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

        public async Task<object> GetUserDetailByUserId(Guid UserId)
        {
            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (userDetail == null)
            {
                return "0";  
            }

            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.Id == UserId);

            var result = new
            {
                UserId = userDetail.UserId,
                Email = user.Email,
                FullName = userDetail.Fullname,
                PhoneNumber = userDetail.PhoneNum,
                Username = user.Username,
                BirthDate = userDetail.BirthDate,
                Address = userDetail.Address,
            };

            return result;
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
                        PhoneNum = userDetailCreateDto.PhoneNum,
                        Address = userDetailCreateDto.Address,
                        IsPrivate = userDetailCreateDto.IsPrivate,
                        BirthDate = userDetailCreateDto.BirthDate,
                    };


                    await _context.UserDetails.AddAsync(userDetail);
                }
                else
                {
                    userDetail.Fullname = userDetailCreateDto.Fullname;
                    userDetail.PhoneNum = userDetailCreateDto.PhoneNum;
                    userDetail.Address = userDetailCreateDto.Address;
                    userDetail.IsPrivate = userDetailCreateDto.IsPrivate;
                    userDetail.BirthDate = userDetailCreateDto.BirthDate;
                }


                await _context.SaveChangesAsync();

                return userDetail;
            }
        }

        public async Task<UserDetail> UpdateUserDetailByUserId(Guid UserId, UserDetailUpdateDto userDetailUpdateDto)
        {
            var existingUserDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (existingUserDetail == null)
            {
                return null;
            }

            existingUserDetail.Fullname = userDetailUpdateDto.Fullname;
            existingUserDetail.PhoneNum = userDetailUpdateDto.PhoneNum;
            existingUserDetail.Address = userDetailUpdateDto.Address;
            existingUserDetail.IsPrivate = userDetailUpdateDto.IsPrivate;
            existingUserDetail.BirthDate = userDetailUpdateDto.BirthDate;

           
            await _context.SaveChangesAsync();

            return existingUserDetail;
        }
    }
}
