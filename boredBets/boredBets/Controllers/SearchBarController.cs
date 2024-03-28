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

            public AllData(List<Horse> horses, List<User> users, List<Jockey> jockeys)
            {
                this.horses = horses;
                this.users = users;
                this.jockeys = jockeys;
            }

            public List<object> GetPaginatedAllData(int page, int perPage)
            {
                var allData = new List<object>();

                allData.AddRange(horses.Select(h => new { Type = "Horse", Data = new HorseData(h.Id, h.Name, h.Stallion) }));
                allData.AddRange(users.Select(u => new { Type = "User", Data = new UserData(u.Id, u.Username) }));
                allData.AddRange(jockeys.Select(j => new { Type = "Jockey", Data = new JockeyData(j.Id, j.Name, j.Male, j.Horses.Any()) }));

                int startIndex = (page - 1) * perPage;


                allData = allData.OrderBy(item =>
                {
                    dynamic d = item;
                    return d.Data.Name.ToString().ToLower();
                }).ToList();

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
            AllData allData = new AllData(
                await _context.Horses.ToListAsync(),
                await _context.Users.ToListAsync(),
                await _context.Jockeys.ToListAsync()
                );

            var paginatedData = allData.GetPaginatedAllData(page, perPage);

            return Ok(paginatedData);
        }
    }

    internal record HorseData(Guid Id, string? Name, bool Stallion);

    internal record UserData(Guid Id, string? Name);

    internal record JockeyData(Guid Id, string? Name, bool Male, bool HasHorse);
}
