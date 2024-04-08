using boredBets.Models.Dtos;
using boredBets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JockeyController : ControllerBase
    {
        private readonly IJockeyInterface jockeyInterface;
        private readonly BoredbetsContext _context;

        public JockeyController(IJockeyInterface jockeyInterface, BoredbetsContext context)
        {
            this.jockeyInterface = jockeyInterface;
            _context = context;
        }

        [HttpPost("JockeyPost")]
        public async Task<ActionResult<Jockey>> Post(JockeyCreateDto jockeyCreateDto)
        {
            return StatusCode(201, await jockeyInterface.Post(jockeyCreateDto));
        }

        [HttpGet("GetAllJockeys")]
        public async Task<ActionResult<Jockey>> GetAllJockey()
        {
            return StatusCode(201, await jockeyInterface.GetAllJockey());
        }

        [HttpGet("GetJockeyById")]
        public async Task<ActionResult<Jockey>> GetJockeyById(Guid JockeyId) 
        {
            var result = await jockeyInterface.GetJockeyById(JockeyId);

            if (result == null)
            {
                NotFound();
            }

            return result;
        }

        [HttpGet("GetJockeyDetailByJockeyId")]
        public async Task<ActionResult<object>> GetJockeyDetailByJockeyId(Guid JockeyId)
        {
            var result = await jockeyInterface.GetJockeyDetailByJockeyId(JockeyId);

            if (result == "0")
            {
                NotFound("Jockey not found");
            }
            return Ok(result);
        }

        [HttpPost("GenerateJockey")]
        public async Task<ActionResult> GenerateJockey(int quantity)
        {
            bool result = await jockeyInterface.GenerateJockey(quantity);
            if (result) 
            {
                return Ok("Succesfully generated "+quantity+" jockey(s)");
            }else 
            {
                return BadRequest("An error occured during jockey generation");
            }
        }
    }
}
