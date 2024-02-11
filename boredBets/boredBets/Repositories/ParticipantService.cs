using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class ParticipantService : IParticipantInterface
    {
        private readonly BoredbetsContext context;

        public ParticipantService(BoredbetsContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Participant>> Get()
        {
            return await context.Participants.ToListAsync();
        }

        public async Task<Participant> Post(Guid RaceId, Guid HorseId, Guid JockeyId, ParticipantCreateDto participantCreateDto)
        {
            try
            {
                var raceid = await context.Races.FirstOrDefaultAsync(x => x.Id == RaceId);
                var horseid = await context.Horses.FirstOrDefaultAsync(x => x.Id == HorseId);
                var jockeyid = await context.Jockeys.FirstOrDefaultAsync(x => x.Id == JockeyId);

                if (raceid == null && horseid == null && jockeyid == null)
                {
                    throw new Exception();
                }

                var participant = new Participant
                {
                    RaceId = RaceId,
                    HorseId = HorseId,
                    JockeyId = JockeyId,
                    Placement = participantCreateDto.Placement,
                };

                await context.Participants.AddAsync(participant);
                await context.SaveChangesAsync();

                return participant;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes.", e);
            }
        }
    }
}
