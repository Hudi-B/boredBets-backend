using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Viewmodels;

namespace boredBets.Repositories.Interface
{
    public interface IParticipantInterface
    {
        Task<Participant> Post(ParticipantDto participantDto);
        Task<IEnumerable<Participant>> Get();
    }
}
