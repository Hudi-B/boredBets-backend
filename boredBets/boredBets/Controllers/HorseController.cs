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
            var freeJockeys =
                from jockey in _context.Jockeys
                where !_context.Horses.Any(horse => horse.JockeyId == jockey.Id)
                select jockey.Id; //selects free jockey that are not connected to any horse

            bool refreshList = false;

            if (freeJockeys.Count() < quantity)
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        StringContent httpContent = new StringContent("");
                        string fullUrl = $"https://boredbetsapidev.azurewebsites.net/api/Jockey/GenerateJockey?quantity=" + (quantity - freeJockeys.Count());
                        HttpResponseMessage response = await client.PostAsync(fullUrl, httpContent);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            // Parse the responseContent to update your list
                            refreshList = true;
                        }
                        else
                        {
                            return StatusCode(500,$"Error getting Jockeys: {response.StatusCode}");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        return StatusCode(500, "HTTP Request Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"General Exception: {ex.Message}");
                    }
                }
            }
            if (refreshList)
            {
                freeJockeys =
                    from jockey in _context.Jockeys
                    where !_context.Horses.Any(horse => horse.JockeyId == jockey.Id)
                    select jockey.Id;
            }


            try
            {
                List<string> maleHorseName = new List<string>();
                List<string> femaleHorseName = new List<string>();
                List<string> Countries = new List<string>();

                #region ReadFile


                string staticData = AppDomain.CurrentDomain.BaseDirectory.ToString() + "../../../staticData/";

                StreamReader sr;
                sr = new StreamReader(staticData + "maleHorses.txt");
                while (!sr.EndOfStream)
                {
                    maleHorseName.Add(sr.ReadLine());
                }
                sr = new StreamReader(staticData + "femaleHorses.txt");
                while (!sr.EndOfStream)
                {
                    femaleHorseName.Add(sr.ReadLine());
                }
                sr = new StreamReader(staticData + "countries.txt");
                while (!sr.EndOfStream)
                {
                    Countries.Add(sr.ReadLine());
                }
                sr.Close();
                #endregion

                Random random = new Random();
                int maleHorseNameCount = maleHorseName.Count();
                int femaleHorseNameCount = femaleHorseName.Count();
                int countriesCount = Countries.Count();
                var freeJockeyIds = freeJockeys.ToList();

                for (int i = 0; i < quantity; i++)
                {
                    bool male = random.Next(2) == 0;
                    string name;
                    if (male)
                    {
                        name = maleHorseName[random.Next(maleHorseNameCount)];
                    }
                    else
                    {
                        name = femaleHorseName[random.Next(femaleHorseNameCount)];
                    }
                    var newHorse = new Horse
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Age = random.Next(4) + 2,
                        Country = Countries[random.Next(countriesCount)],
                        Stallion = male,
                        JockeyId = freeJockeyIds[i]
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
    }
}
