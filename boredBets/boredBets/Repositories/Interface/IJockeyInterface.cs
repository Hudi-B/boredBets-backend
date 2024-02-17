using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IJockeyInterface
    {
        Task<Jockey> Post(JockeyCreateDto jockeyCreateDto);
        Task<IEnumerable<Jockey>> GetAllJockey();
    }
}
