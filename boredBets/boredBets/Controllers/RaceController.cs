using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using boredBets.Repositories.Viewmodels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IRaceInterface _raceInterface;

        public RaceController(IRaceInterface raceInterface)
        {
            _raceInterface = raceInterface;
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

    }
}
