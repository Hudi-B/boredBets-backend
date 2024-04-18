using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace boredBets.Repositories
{
    public class HeadsUpService : IHeadsUpInterface
    {
        private readonly BoredbetsContext _context;
        private readonly IRaceInterface raceInterface;
        private List<Result> horses;

        public HeadsUpService(BoredbetsContext context, IRaceInterface raceInterface)
        {
            _context = context;
            this.raceInterface = raceInterface;
            horses = new List<Result>();
        }
        #region Simulation of race
        public async Task simulateRace()
        {
            var racesToSimulate = await _context.Races
                .Where(x => x.RaceScheduled <= DateTime.UtcNow && x.Participants.Any(y => y.Placement==0))//do we have to check if the race is ended? if so then add the racetime minutes to the datetime.utcnow
                .Include(x => x.Participants)
                    .ThenInclude(p => p.Horse)
                        .ThenInclude(p => p.Jockey)
                .ToListAsync();
            var comparer = new CustomComparer();
            foreach (var race in racesToSimulate)
            {
                var participant = race.Participants.ToList();
                
                foreach (var item in race.Participants)
                {
                    horses.Add(horseChanceCalculate(item.Horse));
                }

                horses.Sort(comparer.Compare);
                horses.Reverse();

                for (int i = 0; i < horses.Count; i++) 
                {
                    var oneHorse = participant.FirstOrDefault(x => x.Horse.Id == horses[i].Horse.Id);
                    oneHorse.Placement = i + 1;
                    await _context.SaveChangesAsync();
                }
            }
        }
        public Result horseChanceCalculate(Horse horse)
        {
            Random rnd = new Random(); 

            double chance = (rnd.Next(5)+8);
            chance += Convert.ToDouble(horse.Jockey.Quality - 5) ;

            if (horse.Stallion)
            {
                chance += 2;
            }
            else
            {
                chance -= 2;
            }

            switch (horse.Age)
            {
                case 2:
                    chance += -2;
                    break;
                case 3:
                    chance += -1;
                    break;
                case 4:
                    chance += 2;   
                    break;
                case 5:
                    chance += 1;
                    break;
                case 6:
                    chance += -1;
                    break;
            }

            var result = new Result {
                Horse = horse,
                Chance = chance/10,
            };

            return result;
        }


        public class Result
        {
            public Horse Horse { get; set; }
            public double Chance { get; set; }
        }

        public class CustomComparer : IComparer<Result>
        {
            Random rnd = new Random();

            public int Compare(Result x, Result y)
            {
                if (x.Chance == y.Chance)
                    return rnd.Next(2) == 0 ? -1 : 1;

                return x.Chance.CompareTo(y.Chance);
            }
        }
        #endregion

        public async Task userBetCalculation(List<Result> horses)
        {
            var raceEnded = await _context.Races
                .Where(r => r.RaceScheduled <= DateTime.UtcNow)
                .Include(r => r.Participants)
                .Include(r => r.UserBets)
                .ToListAsync();

            foreach (var item in raceEnded)
            {
                var participants = item.Participants.OrderBy(p => p.Placement).Take(5).Select(p => p.HorseId).ToList();
                var userBets = item.UserBets.ToList();

                foreach (var userBet in userBets)
                {
                    var bets = new List<Guid> { userBet.First, userBet.Second, userBet.Third, userBet.Fourth, userBet.Fifth };
                    bool isInOrder = bets.SequenceEqual(participants);
                    int isWithoutOrder = bets.Intersect(participants).Count();

                    Dictionary<Horse,double> invertedChance = new Dictionary<Horse, double>(); 


                    for (int i = 0; i < horses.Count; i++)
                    {
                        double inverted = Math.Round(2.5 / horses[i].Chance * 0.9, 2);
                        invertedChance.Add(horses[i].Horse,inverted); 
                        inverted = 0.0;
                    }

                    var bettedHorses = invertedChance.Where(kv => bets.Contains(kv.Key.Id)).ToList().OrderBy(rf=>rf.Value);
                    double money = 0;
                    await Console.Out.WriteLineAsync(   );
                    if (isInOrder)
                    {
                        
                        for (int i = 0; i < bettedHorses.Count(); i++)
                        {
                            money += userBet.BetAmount * (double)bettedHorses.ElementAt(i).Value;
                        }
                    }
                    switch (isWithoutOrder)
                    {
                        case 5:
                            money += userBet.BetAmount * (bettedHorses.Min(rf => rf.Value) * 5);
                            break;
                        case 4:
                            money += userBet.BetAmount * (bettedHorses.Min(rf => rf.Value) * 4);
                            break;
                        case 3:
                            money += userBet.BetAmount * (bettedHorses.Min(rf => rf.Value) * 3);
                            break;
                    }
                }
            }
            await Console.Out.WriteLineAsync(   );
        }



        public async Task checkRace()
        {
            var futureRaces = await raceInterface.GetAllFutureRaces();
            if (futureRaces.Count()  < 20)
            {
                await raceInterface.GenerateRace(40);
            }
            Console.Write("a");
        }

        public async Task<List<Result>> GetResults()
        {
            return horses;
        }
    }
}
