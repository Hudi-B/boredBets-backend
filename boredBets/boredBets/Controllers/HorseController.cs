using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorseController : ControllerBase
    {
        private readonly IHorseInterface horseInterface;

        public HorseController(IHorseInterface horseInterface)
        {
            this.horseInterface = horseInterface;
        }

        [HttpPost("HorsePost")]
        public async Task<ActionResult<Horse>> Post(HorseCreateDto horseCreateDto)
        {
            return StatusCode(201, await horseInterface.Post(horseCreateDto));
        }

        [HttpGet("GetAllHorses")]
        public async Task<ActionResult<Horse>> GetAllHorse() 
        {
            return StatusCode(201, await horseInterface.GetAllHorse());
        }

        [HttpGet("GetHorseById")]
        public async Task<ActionResult<Horse>> GetHorseById(Guid HorseId)
        {
            var result = await horseInterface.GetHorseById(HorseId);

            if (result == null)
            {
                NotFound();
            }

            return result;
        }

        [HttpDelete("DeleteHorseAndJockeyById")]
        public async Task<ActionResult<Horse>> DeleteHorseAndJockeyById(Guid Id) 
        {
            var result = await horseInterface.DeleteHorseAndJockeyBy(Id);
            if(result == null) 
            {
                NotFound(); 
            }
            return Ok(result);
        }
    }
}
