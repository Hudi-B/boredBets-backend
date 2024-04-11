using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IHeadsUpInterface
    {
        Task simulateRace();
    }
}
