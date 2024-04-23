using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace boredBets.Repositories
{
    public class UserBetService : IUserBetInterface
    {
        private readonly BoredbetsContext _context;

        public UserBetService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<UserBet> GetUserBetsById(Guid Id)
        {
            var id = await _context.UserBets.FirstOrDefaultAsync(x => x.Id == Id);

            if (id == null)
            {
                return null;
            }

            return id;
        }

        public async Task<IEnumerable<UserBetWithHorsesAndTrack>> GetAllUserBetsByUserId(Guid userId)
        {
            var userBets = await _context.UserBets
                                          .Where(x => x.UserId == userId)
                                          .Include(x => x.Race)
                                              .ThenInclude(race => race.Track)
                                          .Include(x => x.Race)
                                              .ThenInclude(race => race.Participants)
                                              .ThenInclude(participant => participant.Horse)
                                          .ToListAsync();

            if (userBets == null || !userBets.Any())
            {
                return Enumerable.Empty<UserBetWithHorsesAndTrack>();
            }

            var userBetDetails = userBets.Select(userBet => new UserBetWithHorsesAndTrack
            {
                Id = userBet.Id,
                UserId = userBet.UserId,
                RaceId = userBet.RaceId,
                First = userBet.First,
                Second = userBet.Second,
                Third = userBet.Third,
                Fourth = userBet.Fourth,
                Fifth = userBet.Fifth,
                BetAmount = userBet.BetAmount,
                BetTypeId = userBet.BetTypeId,
                BetType = userBet.BetType,
                TrackName = userBet.Race?.Track?.Name,
                Horses = userBet.Race?.Participants
                                    .Where(participant => participant.Horse != null &&
                                                          (participant.Horse.Id == userBet.First ||
                                                           participant.Horse.Id == userBet.Second ||
                                                           participant.Horse.Id == userBet.Third ||
                                                           participant.Horse.Id == userBet.Fourth ||
                                                           participant.Horse.Id == userBet.Fifth))
                                    .Select(participant => new HorseDetails
                                    {
                                        Id = participant.Horse.Id,
                                        Name = participant.Horse.Name,
                                        Stallion = participant.Horse.Stallion
                                    })
                                    .ToList()
            }).ToList();

            return userBetDetails;
        }
        #region GetAllUserBetsByUserId Classes
        public class UserBetWithHorsesAndTrack
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid? RaceId { get; set; }
            public Guid First { get; set; }
            public Guid Second { get; set; }
            public Guid Third { get; set; }
            public Guid Fourth { get; set; }
            public Guid Fifth { get; set; }
            public int BetAmount { get; set; }
            public int? BetTypeId { get; set; }
            public BetType? BetType { get; set; }
            public string TrackName { get; set; }
            public List<HorseDetails> Horses { get; set; }
        }

        public class HorseDetails
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool Stallion { get; set; }
        }
        #endregion
        public async Task<object> Post(UserBetCreateDto userBetCreateDto)
        {
            var horses = new List<Guid> { userBetCreateDto.First, userBetCreateDto.Second, userBetCreateDto.Third, userBetCreateDto.Fourth, userBetCreateDto.Fifth };

            var missingParticipant = horses.FirstOrDefault(horseId =>
                !_context.Participants.Any(x => x.RaceId == userBetCreateDto.RaceId && x.HorseId == horseId));

            if (missingParticipant != Guid.Empty)
            {
                return "1";
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userBetCreateDto.UserId);
            var usercard = await _context.UserCards.AnyAsync(u => u.UserId == user.Id);
            var inTheFuture = await _context.Races.FirstOrDefaultAsync(x =>x.Id == userBetCreateDto.RaceId && x.RaceScheduled > DateTime.UtcNow.AddMinutes(5));

            if (user == null || inTheFuture == null || !usercard)
            {
                return "2";
            }

            if (userBetCreateDto.BetAmount > user.Wallet)
            {
                return "3";
            }

            var userbet = new UserBet
            {
                Id = Guid.NewGuid(),
                UserId = userBetCreateDto.UserId,
                RaceId = userBetCreateDto.RaceId,
                First = userBetCreateDto.First,
                Second = userBetCreateDto.Second,
                Third = userBetCreateDto.Third,
                Fourth = userBetCreateDto.Fourth,
                Fifth = userBetCreateDto.Fifth,
                BetAmount = userBetCreateDto.BetAmount,
                BetTypeId = userBetCreateDto.BetTypeId,
            };

            var transaction = new Transaction 
            {
                UserId = userBetCreateDto.UserId,
                Amount = userBetCreateDto.BetAmount,
                Created = DateTime.UtcNow,
                TransactionType = 2,
            };

            user.Wallet -= userbet.BetAmount;

            await _context.Transactions.AddAsync(transaction);

            await _context.UserBets.AddAsync(userbet);

            await _context.SaveChangesAsync();

            return "Success";
        }

        public async Task<UserBet> DeleteUserBetById(Guid Id)
        {
            var id = await _context.UserBets.FirstOrDefaultAsync(x=>x.Id==Id);

            if (id == null)
            {
                return null;
            }

            _context.UserBets.Remove(id);
            await _context.SaveChangesAsync();
            
            return id;
        }
    }
}
