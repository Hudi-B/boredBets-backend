using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IUserDetailInterface
    {
        Task<UserDetail> Post(Guid Id,UserDetailCreateDto userDetailCreateDto);
        Task<UserDetail> GetUserDetailByUserId(Guid UserId);
        Task<UserDetail> UpdateUserDetailByUserId(Guid UserId, UserDetailUpdateDto userDetailUpdateDto);
    }
}
