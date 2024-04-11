using boredBets.Models.Dtos;
using boredBets.Models;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

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
            //handleBets()
            }
            catch {
            }

            return StatusCode(201);
        }
    }
}
