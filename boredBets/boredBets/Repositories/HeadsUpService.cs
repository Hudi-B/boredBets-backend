using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
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
        public async Task simulateRace()
        {
            var racesToSimulate = await _context.Races
                .Where(x => x.RaceScheduled <= DateTime.UtcNow && x.Participants.Any(y => y.Placement==0))
                .Include(x => x.Participants)
                    .ThenInclude(p => p.Horse)
                        .ThenInclude(p => p.Jockey)
                .ToListAsync();
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


        public async Task checkRace()
        {
            var futureRaces = await raceInterface.GetAllFutureRaces();
            if (futureRaces.Count() < 20)
            {
                await raceInterface.GenerateRace(40);
            }
        }

    }
}
