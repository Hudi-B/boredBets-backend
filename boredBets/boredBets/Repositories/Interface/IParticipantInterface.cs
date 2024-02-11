using boredBets.Models;
using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IParticipantInterface
    {
        Task<Participant> Post(Guid RaceId, Guid HorseId, Guid JockeyId, ParticipantCreateDto participantCreateDto);
        Task<IEnumerable<Participant>> Get();
    }
}
