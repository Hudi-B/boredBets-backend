using boredBets.Models.Dtos;
using boredBets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using boredBets.Repositories.Interface;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackInterface trackInterface;

        public TrackController(ITrackInterface trackInterface)
        {
            this.trackInterface = trackInterface;
        }

        [HttpPost("TrackPost")]
        public async Task<ActionResult<Track>> Post(TrackCreateDto trackCreateDto)
        {
            return StatusCode(201, await trackInterface.Post(trackCreateDto));
        }

        [HttpGet("GetAllTracks")]
        public async Task<ActionResult<Track>> GetAllTrack()
        {
            return StatusCode(201, await trackInterface.GetAllTrack());
        }

    }
}
