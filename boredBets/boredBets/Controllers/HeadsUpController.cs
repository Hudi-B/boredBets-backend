using boredBets.Models.Dtos;
using boredBets.Models;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using boredBets.Repositories;
using System.Collections.Generic;
using static boredBets.Repositories.HeadsUpService;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadsUpController : ControllerBase
    {
        private readonly BoredbetsContext _context;
        private readonly IHeadsUpInterface headsUp;

        public HeadsUpController(BoredbetsContext context, IHeadsUpInterface headsUp)
        {
            _context = context;
            this.headsUp = headsUp;
        }


        [HttpGet("HeadsUp")]
        public async Task<ActionResult> Refresh()
        {
            await headsUp.simulateRace();
            await headsUp.checkRace();
            await headsUp.deleteFakeUsers();

            List<Result> raceResult = await headsUp.GetResults();

            var result = await headsUp.userBetCalculation(raceResult);

            if (result == "0")
            {
                return Ok("Bets doesn't match");
            }
            await Console.Out.WriteLineAsync(   );
            return Ok(result);
        }
    }
}
