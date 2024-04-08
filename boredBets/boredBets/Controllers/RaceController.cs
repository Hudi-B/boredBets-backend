using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IRaceInterface _raceInterface;
        private readonly BoredbetsContext _context;

        public RaceController(IRaceInterface raceInterface, BoredbetsContext context)
        {
            _raceInterface = raceInterface;
            _context = context;
        }

        [HttpPost("RacePost")]
        public async Task<ActionResult<Race>> Post(RaceCreateDto raceCreateDto)
        {
            var result = await _raceInterface.Post(raceCreateDto);

            if (result=="0")
            {
                NotFound("Track not found");
            }
            return Ok(result);
        }

        [HttpGet("GetFivePreviousRaces")]
        public async Task<ActionResult<Race>> GetAlreadyHappenedRaces()
        {
            return StatusCode(201, await _raceInterface.GetAlreadyHappenedRaces());
        }

        [HttpGet("GetAllHappendRaces")]
        public async Task<ActionResult<Race>> GetAllHappendRaces()
        {
            return StatusCode(201, await _raceInterface.GetAllHappendRaces());
        }

        [HttpGet("GetFiveFutureRaces")]
        public async Task<ActionResult<Race>> GetFutureRaces()
        {
            return StatusCode(201, await _raceInterface.GetFutureRaces());
        }

        [HttpGet("GetAllFutureRaces")]
        public async Task<ActionResult<Race>> GetAllFutureRaces()
        {
            return StatusCode(201, await _raceInterface.GetAllFutureRaces());
        }
        [HttpGet("GetByRaceId")]
        public async Task<ActionResult<Race>> GetByRaceId(Guid Id)
        {
            var result = await _raceInterface.GetByRaceId(Id);

            if (result == "0")
            {
                NotFound("Race not found");
            }
            return Ok(result);
        }

        [HttpGet("GetByCountry")]
        public async Task<ActionResult<Race>> GetByCountry(string country)
        {
            return StatusCode(201, await _raceInterface.GetByCountry(country));
        }

        [HttpDelete("DeleteRaceById")]
        public async Task<ActionResult<Race>> DeleteRaceById(Guid Id) 
        {
            var result = await _raceInterface.DeleteRaceById(Id);

            if (result == null)
            {
                NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GenerateRaces")]
        public async Task<ActionResult<int>> GenerateRace(int quantity)
        {
            DateTime oneDayAgo = DateTime.UtcNow.AddDays(-1);

            var horsesWithoutRecentRaces = await _context.Horses
                .Where(h => !h.Participants.Any(p => p.Race.RaceScheduled >= oneDayAgo))
                .ToListAsync();


            Random rnd = new Random();
            int rainValue = rnd.Next(2);

            if (horsesWithoutRecentRaces.Count() < quantity*20)
            {
                return BadRequest($"Not enough free horses. You need {(quantity*20)-horsesWithoutRecentRaces.Count()}");
            }

            List<string> Tracks = new List<string>();

            #region ReadFile
            string staticData = AppDomain.CurrentDomain.BaseDirectory.ToString() + "../../../staticData/";
            StreamReader sr = new StreamReader(staticData + "trackNames.txt");
            while (!sr.EndOfStream)
            {
                Tracks.Add(sr.ReadLine());
            }
            sr.Close();
            #endregion


            var latestRace = _context.Races.FirstOrDefault(r => r.RaceScheduled > DateTime.UtcNow);

            var trackz = await _context.Tracks.ToListAsync();
            int maxTrakc = trackz.Count();
            for (int i = 0; i < quantity; i++)
            {
                Guid raceId = Guid.NewGuid();

                var race = new Race
                {
                    Id = raceId,
                    RaceTime = rnd.Next(3, 11),
                    RaceScheduled = latestRace != null ? latestRace.RaceScheduled.AddMinutes((i+1)*5) :  DateTime.UtcNow.AddMinutes((i+1)*5),
                    Rain = Convert.ToBoolean(rainValue),
                    TrackId = trackz[rnd.Next(maxTrakc)].Id
                };

                for (int j = 0; j < 20; j++)
                {
                    var selectedHorseIndex = rnd.Next(horsesWithoutRecentRaces.Count);
                    var selectedHorse = horsesWithoutRecentRaces[selectedHorseIndex];

                    var participate = new Participant
                    {
                        Id = Guid.NewGuid(),
                        RaceId = raceId,
                        HorseId = selectedHorse.Id,
                        Placement = 0
                    };

                    horsesWithoutRecentRaces.RemoveAt(selectedHorseIndex);


                    _context.Participants.Add(participate);
                }

                _context.Races.Add(race);
            }

            await _context.SaveChangesAsync();

            return Ok(quantity + " races generated successfully");
        }
    }
}
