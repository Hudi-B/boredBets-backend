using boredBets.Models.Dtos;
using boredBets.Models;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using boredBets.Repositories;
using System.Collections.Generic;
using static boredBets.Repositories.HeadsUpService;

namespace boredBets.Controllers
{
    public class HeadsUpController : Controller
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
            try {
                await headsUp.simulateRace();
                await headsUp.checkRace();

                List<Result> raceResult = await headsUp.GetResults();

                await headsUp.userBetCalculation(raceResult);
                await Console.Out.WriteLineAsync(   );
            }
            catch {
            }

            return StatusCode(201);
        }
    }
}
