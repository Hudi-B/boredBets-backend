using boredBets.Models;
using boredBets.Models.Dtos;
using System.Collections.Generic;

namespace boredBets.Repositories.Interface
{
    public interface IUserBetInterface
    {
        Task<UserBet> Post(UserBetCreateDto userBetCreateDto);
        Task<IEnumerable<UserBet>> GetAllUserBetsByUserId(Guid UserId);
        Task<IEnumerable<UserBet>> GetAllUserBetsById(Guid Id);
    }
}
