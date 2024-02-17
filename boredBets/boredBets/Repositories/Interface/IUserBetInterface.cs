using boredBets.Models;
using boredBets.Models.Dtos;
using System.Collections.Generic;

namespace boredBets.Repositories.Interface
{
    public interface IUserBetInterface
    {
        Task<UserBet> Post(Guid Id,Guid HorseId,Guid RaceId,UserBetCreateDto userBetCreateDto);
        Task<IEnumerable<UserBet>> GetAllUserBetsByUserId(Guid UserId);
    }
}
