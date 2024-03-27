using boredBets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    

    public class SearchBarController : ControllerBase
    {
        public class AllData
        {
            public List<Horse> horses = new List<Horse>();
            public List<User> users = new List<User>();
            public List<Jockey> jockeys = new List<Jockey>();

            public void AddHorse(Horse horse)
            {
                horses.Add(horse);
            }

            public void AddUser(User user)
            {
                users.Add(user);
            }

            public void AddJockey(Jockey jockey)
            {
                jockeys.Add(jockey);
            }

            public List<object> GetPaginatedAllData(int page, int perPage)
            {
                List<object> allData = new List<object>();
                allData.AddRange(horses.Select(h => new { Type = "Horse", Data = h }));
                allData.AddRange(users.Select(u => new { Type = "User", Data = u }));
                allData.AddRange(jockeys.Select(j => new { Type = "Jockey", Data = j }));

                int startIndex = (page - 1) * perPage;

                var search = allData.Skip(startIndex).Take(perPage).ToList();

                return search;
            }

        }
        private readonly BoredbetsContext _context;

        public SearchBarController(BoredbetsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int page = 1, int perPage = 60)
        {
            var horses = await _context.Horses.ToListAsync(); 
            var users = await _context.Users.ToListAsync();
            var jockeys = await _context.Jockeys.ToListAsync();

            AllData allData = new AllData();

            foreach (var horse in horses)
            {
                allData.AddHorse(horse);
            }

            foreach (var user in users)
            {
                allData.AddUser(user);
            }

            foreach (var jockey in jockeys)
            {
                allData.AddJockey(jockey);
            }

            var paginatedData = allData.GetPaginatedAllData(page, perPage);

            return Ok(paginatedData);
        }
    }

}
