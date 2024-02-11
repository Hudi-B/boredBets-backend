using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
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

        [HttpPost]
        public async Task<ActionResult<Race>> Post(Guid TrackId, RaceCreateDto raceCreateDto)
        {
            return StatusCode(201, await _raceInterface.Post(TrackId, raceCreateDto));
        }
        [HttpGet("GetFiveAlreadyHappenedRaces")]
        public async Task<ActionResult<Race>> GetAlreadyHappenedRaces()
        {
            return StatusCode(201, await _raceInterface.GetAlreadyHappenedRaces());
        }
        [HttpGet("GetAllHappendRaces")]
        public async Task<ActionResult<Race>> GetAllHappendRaces()
        {
            return StatusCode(201, await _raceInterface.GetAllHappendRaces());
        }
        [HttpGet("GetFutureFiveRaces")]
        public async Task<ActionResult<Race>> GetFutureRaces()
        {
            return StatusCode(201, await _raceInterface.GetFutureRaces());
        }
        [HttpGet("GetAllFutureFiveRaces")]
        public async Task<ActionResult<Race>> GetAllFutureRaces()
        {
            return StatusCode(201, await _raceInterface.GetAllFutureRaces());
        }
    }
}
