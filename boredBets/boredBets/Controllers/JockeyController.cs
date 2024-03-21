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
            try
            {
                List<string> familyNames = new List<string>();

                List<string> maleNames = new List<string>();
                List<string> femaleNames = new List<string>();

                List<string> maleMiddleNames = new List<string>();
                List<string> femaleMiddleNames = new List<string>();

                #region ReadFile
                string staticData = AppDomain.CurrentDomain.BaseDirectory.ToString() + "../../../staticData/";

                StreamReader sr;
                sr = new StreamReader(staticData + "familyNames.txt");
                while (!sr.EndOfStream)
                {
                    familyNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "maleNames.txt");
                while (!sr.EndOfStream)
                {
                    maleNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "femaleNames.txt");
                while (!sr.EndOfStream)
                {
                    femaleNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "maleMiddleNames.txt");
                while (!sr.EndOfStream)
                {
                    maleMiddleNames.Add(sr.ReadLine());
                }

                sr = new StreamReader(staticData + "femaleMiddleNames.txt");
                while (!sr.EndOfStream)
                {
                    femaleMiddleNames.Add(sr.ReadLine());
                }
                sr.Close();
                #endregion

                Random random = new Random();
                for (int i = 0; i < quantity; i++)
                {
                    bool male = random.Next(2) == 0;
                    string name = familyNames[random.Next(familyNames.Count())];

                    if (male)
                    {
                        if (random.Next(10) == 6)
                        {
                            name += " " + maleMiddleNames[random.Next(maleMiddleNames.Count())];
                        }
                        name += " " + maleNames[random.Next(maleNames.Count())];
                    }
                    else
                    {
                        if (random.Next(10) == 6)
                        {
                            name += " " + femaleMiddleNames[random.Next(femaleMiddleNames.Count())];
                        }
                        name += " " + femaleNames[random.Next(femaleNames.Count())];
                    }

                    var newJockey = new Jockey
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Quality = random.Next(10) + 1,
                        Male = male
                    };

                    await _context.Jockeys.AddAsync(newJockey);
                }

                await _context.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
