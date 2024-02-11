using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IUserBetInterface
    {
        Task<UserBet> Post(Guid Id,Guid HorseId,Guid RaceId,UserBetCreateDto userBetCreateDto);
    }
}
