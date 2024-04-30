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
        Task<object> GetAllHappendRaces(int page, int perPage);

        Task<object> GetByRaceId(Guid RaceId);
        Task<IEnumerable<object>> GetByAllRaces();
        Task<IEnumerable<GetByCountryViewModel>> GetByCountry(string Country);
        Task<object> DeleteRaceById(Guid Id);
        Task<bool> GenerateRace(int quantity);
        Task<string> ForceStartRaceByRaceId(Guid RaceId);
    }
}
