using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace boredBets.Repositories
{
    public class HeadsUpService : IHeadsUpInterface
    {
        private readonly BoredbetsContext _context;
        private readonly IRaceInterface raceInterface;

        public HeadsUpService(BoredbetsContext context, IRaceInterface raceInterface)
        {
            _context = context;
            this.raceInterface = raceInterface;
        }
        #region Simulation of race
        public async Task<List<Result>> simulateRace()
        {
            var racesToSimulate = await _context.Races
                .Where(x => x.RaceScheduled <= DateTime.UtcNow && x.Participants.Any(y => y.Placement==0))
                .Include(x => x.Participants)
                    .ThenInclude(p => p.Horse)
                        .ThenInclude(p => p.Jockey)
                .ToListAsync();

            var allRaceResults = new List<Result>();

            var comparer = new CustomComparer();
            foreach (var race in racesToSimulate)
            {
                
                var participant = race.Participants.ToList();
                List<Result> horses = new List<Result>();
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
                }
                allRaceResults.AddRange(horses);
                
                await _context.SaveChangesAsync();
                
            }

            await userBetCalculation(allRaceResults);

            return allRaceResults;
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

        public async Task<object> userBetCalculation(List<Result> allRaceResults)
        {
            var winnerBets = new List<object>();
            if (allRaceResults.Count()==0)
            {
                return winnerBets;
            }
            var racesEnded = await _context.Races
                .Where(r => r.RaceScheduled <= DateTime.UtcNow)
                .Include(r => r.Participants)
                .Include(r => r.UserBets)
                .ToListAsync();

            

            foreach (var race in racesEnded)
            {
                var participants = race.Participants.OrderBy(p => p.Placement).Take(5).Select(p => p.HorseId).ToList();
                var userBets = race.UserBets.ToList();

                if (userBets.Any())
                {
                    var invertedChance = new Dictionary<Guid, double>();

                    foreach (var result in allRaceResults)
                    {
                        double inverted = Math.Round(2.5 / result.Chance * 0.9, 2);
                        invertedChance.Add(result.Horse.Id, inverted);
                    }

                    foreach (var userBet in userBets)
                    {
                        var bets = new List<Guid> { userBet.First, userBet.Second, userBet.Third, userBet.Fourth, userBet.Fifth };
                        bool isInOrder = bets.SequenceEqual(participants);
                        int isWithoutOrder = bets.Intersect(participants).Count();

                        var bettedHorses = invertedChance.Where(kv => bets.Contains(kv.Key)).OrderBy(rf => rf.Value);
                        decimal moneyWon = 0;
                        if (bettedHorses.Count() > 2)
                        {
                            double betMultiplier = 0;

                            if (isInOrder)
                            {
                                for (int i = 0; i < bettedHorses.Count(); i++)
                                {
                                    betMultiplier += bettedHorses.ElementAt(i).Value;
                                }
                            }
                            else
                            {
                                switch (isWithoutOrder)
                                {
                                    case 5:
                                        betMultiplier = bettedHorses.Min(rf => rf.Value) * 5;
                                        break;
                                    case 4:
                                        betMultiplier = bettedHorses.Min(rf => rf.Value) * 4;
                                        break;
                                    case 3:
                                        betMultiplier = bettedHorses.Min(rf => rf.Value) * 3;
                                        break;
                                }
                            }
                            var Profit = new UserDetail().Profit;
                            moneyWon = userBet.BetAmount * (decimal)betMultiplier;
                            if (moneyWon > 0)
                            {
                                userBet.User.Wallet += moneyWon;
                                Profit += moneyWon;
                                await _context.SaveChangesAsync();
                            }
                            Profit-=userBet.BetAmount;
                            await _context.SaveChangesAsync();

                            
                            await Console.Out.WriteLineAsync();
                        }
                        var betInfo = new
                        {
                            User = userBet.UserId,
                            BetAmount = userBet.BetAmount,
                            Winnings = moneyWon
                        };
                        winnerBets.Add(betInfo);
                        await Console.Out.WriteLineAsync();
                    }
                }
            }
            await Console.Out.WriteLineAsync();
            return winnerBets;
        }




        public async Task checkRace()
        {
            var futureRaces = await raceInterface.GetAllFutureRaces();
            if (futureRaces.Count()  < 20)
            {
                await raceInterface.GenerateRace(40);
            }
        }

        public async Task<List<Result>> GetResults()
        {
            return simulateRace().Result;
        }
    }
}