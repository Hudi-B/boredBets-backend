using boredBets.Models;
using boredBets.Models.Dtos;
using static boredBets.Repositories.HeadsUpService;

namespace boredBets.Repositories.Interface
{
    public interface IHeadsUpInterface
    {
        Task<List<Result>> simulateRace();
        Task checkRace();
        Task deleteFakeUsers();
        Task<List<HeadsUpService.Result>> GetResults();
        Task<object> userBetCalculation(List<HeadsUpService.Result> result);
    }
}
