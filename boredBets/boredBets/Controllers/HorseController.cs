using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorseController : ControllerBase
    {
        private readonly IHorseInterface horseInterface;

        private readonly BoredbetsContext _context;

        public HorseController(IHorseInterface horseInterface, BoredbetsContext context)
        {
            this.horseInterface = horseInterface;
            _context = context;
        }

        [HttpPost("HorsePost")]
        public async Task<ActionResult<Horse>> Post(HorseCreateDto horseCreateDto)
        {
            return StatusCode(201, await horseInterface.Post(horseCreateDto));
        }

        [HttpGet("GetAllHorses")]
        public async Task<ActionResult<HorseContentDto>> GetAllHorse() 
        {
            return StatusCode(201, await horseInterface.GetAllHorse());
        }

        [HttpGet("GetHorseById")]
        public async Task<ActionResult<HorseContentDto>> GetHorseById(Guid HorseId)
        {
            var result = await horseInterface.GetHorseById(HorseId);

            if (result == null)
            {
                NotFound();
            }

            return result;
        }

        [HttpGet("GetHorseDetailByHorseId")]
        public async Task<ActionResult<object>> GetHorseDetailByHorseId(Guid HorseId)
        {
            var result = await horseInterface.GetHorseDetailByHorseId(HorseId);

            if (result == "0")
            {
                NotFound("Horse not found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteHorseById")]
        public async Task<ActionResult<Horse>> DeleteHorseById(Guid Id) 
        {
            var result = await horseInterface.DeleteHorseById(Id);
            if (result == "0")
            {
                NotFound("Horse not found");
            }
            else if (result == "1")
            {
                NotFound("Jockey not found");
            }
            return Ok(result);
        }



        [HttpPost("GenerateHorses")]
        public async Task<ActionResult<int>> GenerateHorse(int quantity)
        {
            try
            {
                bool result = await horseInterface.GenerateHorse(quantity);
                if (result) {
                    return StatusCode(201, "Succesfully generated " + quantity + " horse(s)");
                }
                else
                {
                    return StatusCode(501, "Unknown error occured");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex);
            }
        }
    }
}
