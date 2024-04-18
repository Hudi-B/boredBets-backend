using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IHeadsUpInterface
    {
        Task simulateRace();
        Task checkRace();

        Task<List<HeadsUpService.Result>> GetResults();
        Task userBetCalculation(List<HeadsUpService.Result> result);
    }
}
