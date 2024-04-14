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
        private readonly IHorseInterface _horseInterface;
        private readonly BoredbetsContext _context;

        public RaceController(IRaceInterface raceInterface, IHorseInterface horseInterface, BoredbetsContext context)
        {
            _raceInterface = raceInterface;
            _horseInterface = horseInterface;
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
        public async Task<ActionResult<Race>> GetAllHappendRaces(int page = 1, int perPage = 10)
        {
            return StatusCode(201, await _raceInterface.GetAllHappendRaces(page,perPage));
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
            if (await _raceInterface.GenerateRace(quantity))
            {

                return Ok(quantity+" races generated succesfully!");
            }
            else
            {
                return BadRequest("There was an error during race generation");
            }
        }
        

    }
}
