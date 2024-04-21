using boredBets.Models;
using boredBets.Models.Dtos;
using System.Collections.Generic;
using static boredBets.Repositories.UserBetService;

namespace boredBets.Repositories.Interface
{
    public interface IUserBetInterface
    {
        Task<object> Post(UserBetCreateDto userBetCreateDto);
        Task<IEnumerable<UserBetWithHorsesAndTrack>> GetAllUserBetsByUserId(Guid UserId);
        Task<UserBet> GetUserBetsById(Guid Id);
        Task<UserBet> DeleteUserBetById(Guid Id);
    }
}
