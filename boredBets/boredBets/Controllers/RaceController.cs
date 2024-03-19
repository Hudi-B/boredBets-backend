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
            return StatusCode(201, await _raceInterface.Post(raceCreateDto));
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
            return StatusCode(201, await _raceInterface.GetByRaceId(Id));
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
        public async Task<ActionResult<int>> GenerateRace(RaceGenerate generate) 
        {
            var freeHorses = await _context.Horses
                                                  .Take(20) 
                                                  .ToListAsync();

            if (freeHorses.Count >= 20) 
            {
                var trackIdExist = _context.Tracks.FirstOrDefaultAsync(x => x.Id == generate.TrackId);

                if (trackIdExist==null)
                {
                    return BadRequest("Track doesn't exist");
                }

                Guid raceId = Guid.NewGuid();

                var race = new Race
                {
                    Id = raceId,
                    //RaceTime = noidea
                    RaceScheduled = DateTime.UtcNow,
                    //Weather = in dto??
                    TrackId = generate.TrackId,

                };

                var participate = new Participant
                {
                    Id = Guid.NewGuid(),
                    RaceId = raceId,
                    //HorseId = huhh lost contact with earth
                    Placement = 0 // I ON KNOE 
                };
            }
            return BadRequest("Not enough horse");//we could use generate horse here GenerateHorse(20-freeHorses.Count)

        }
        

    }
}
