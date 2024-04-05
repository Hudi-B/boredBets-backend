using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Viewmodels;

namespace boredBets.Repositories.Interface
{
    public interface IRaceInterface
    {
        Task<object> Post(RaceCreateDto raceCreateDto);
        Task<IEnumerable<FiveRaceViewModel>> GetAlreadyHappenedRaces();
        Task<IEnumerable<FiveRaceViewModel>> GetFutureRaces();
        Task<IEnumerable<AllHappendRaceViewModel>> GetAllFutureRaces();
        Task<IEnumerable<AllHappendRaceViewModel>> GetAllHappendRaces();

        Task<object> GetByRaceId(Guid RaceId);
        Task<IEnumerable<GetByCountryViewModel>> GetByCountry(string Country);
        Task<object> DeleteRaceById(Guid Id);
    }
}
