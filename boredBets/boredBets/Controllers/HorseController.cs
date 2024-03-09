using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("GenerateHorses")]

        public async Task<ActionResult<int>> GenerateHorse(int quantity)
        {
            var freeJockeys =
                from jockey in _context.Jockeys
                where !_context.Horses.Any(horse => horse.JockeyId == jockey.Id)
                select jockey.Id; //selects free jockey that are not connected to any horse

            if (freeJockeys.Count() >= quantity)
            {
                try
                {
                    List<string> horseNames = new List<string>();

                    #region ReadFile
                    string Path = "../../../staticData/maleHorses.txt";
                    string absolutePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + Path;

                    StreamReader sr = new StreamReader(absolutePath);
                    while (!sr.EndOfStream)
                    {
                        horseNames.Add(sr.ReadLine());
                    }
                    #endregion

                    Random random = new Random();
                    for (int i = 0; i < quantity; i++)
                    {
                        var newHorse = new Horse
                        {
                            Id = Guid.NewGuid(),
                            Name = horseNames[random.Next(horseNames.Count)],
                            Age = random.Next(4) + 2,
                            Stallion = random.Next(2) == 0,
                            JockeyId = freeJockeys.ToList()[i]
                        };

                        await _context.Horses.AddAsync(newHorse);
                    }

                    await _context.SaveChangesAsync();

                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
            else
            {
                return StatusCode(500, "Jockey quantity not sufficient. Available jockeys: "+freeJockeys.Count());
            }

        }



    }
}
