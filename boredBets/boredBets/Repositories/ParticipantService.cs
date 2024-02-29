using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
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

        public async Task<Participant> Post(ParticipantDto participantDto)
        {
            try
            {
                var raceid = await context.Races.FirstOrDefaultAsync(x => x.Id == participantDto.RaceId);
                var horseid = await context.Horses.FirstOrDefaultAsync(x => x.Id == participantDto.HorseId);
                var jockeyid = await context.Jockeys.FirstOrDefaultAsync(x => x.Id == participantDto.JockeyId);

                if (raceid == null && horseid == null && jockeyid == null)
                {
                    throw new Exception();
                }

                var participant = new Participant
                {
                    RaceId = participantDto.RaceId,
                    HorseId = participantDto.HorseId,
                    JockeyId = participantDto.JockeyId,
                    Placement = participantDto.Placement,
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
