using boredBets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            public (List<object>, int) GetPaginatedAllData(int page, int perPage)
            {

                var allData = new List<object>();

                allData.AddRange(horses.Select(h => new { Type = "Horse", Data = new HorseData(h.Id, h.Name, h.Stallion, h.Age) }));
                allData.AddRange(users.Select(u => new { Type = "User", Data = new UserData(u.Id, u.Username, u.UserDetail.IsPrivate.Equals(true)) }));
                allData.AddRange(jockeys.Select(j => new { Type = "Jockey", Data = new JockeyData(j.Id, j.Name, j.Male, j.Horses.Any()) }));

                int totalCount = allData.Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage);

                int startIndex = (page - 1) * perPage;

                allData = allData.OrderBy(item =>
                {
                    dynamic d = item;
                    return d.Data.Name.ToString().ToLower();
                }).ToList();

                var search = allData.Skip(startIndex).Take(perPage).ToList();

                return (search, totalPages);
            }

            public (List<object>, int) GetPaginatedFilteredData(Filters filters, string filteredGroup,int page, int perPage)
            {

                var allData = new List<object>();

                allData.AddRange(horses.Select(h => new { Type = "Horse", Data = new HorseData(h.Id, h.Name, h.Stallion, h.Age) }));
                allData.AddRange(users.Select(u => new { Type = "User", Data = new UserData(u.Id, u.Username, u.UserDetail.IsPrivate.Equals(true)) }));
                allData.AddRange(jockeys.Select(j => new { Type = "Jockey", Data = new JockeyData(j.Id, j.Name, j.Male, j.Horses.Any()) }));



                switch (filteredGroup)
                {
                    case "Jockey":
                        allData = allData.Where(entry => entry.GetType().GetProperty("Type")?.GetValue(entry)?.ToString() == "Jockey").ToList();
                        if (filters.JockeyFilter.Male != filters.JockeyFilter.Female)
                        {
                            allData = allData.Where(entry =>
                            {
                                var dataProperty = entry.GetType().GetProperty("Data");
                                if (dataProperty != null)
                                {
                                    var dataValue = dataProperty.GetValue(entry);
                                    if (dataValue is JockeyData data && data.Male == filters.JockeyFilter.Male)
                                        return true;
                                }
                                return false;
                            }).ToList();
                        }

                        if (filters.JockeyFilter.hashorse != filters.JockeyFilter.hasnohorse)
                        {
                            allData = allData.Where(entry =>
                            {
                                var dataProperty = entry.GetType().GetProperty("Data");
                                if (dataProperty != null)
                                {
                                    var dataValue = dataProperty.GetValue(entry);
                                    if (dataValue is JockeyData data && data.hashorse == filters.JockeyFilter.hashorse)
                                        return true;
                                }
                                return false;
                            }).ToList();
                        }
                        break;




                    case "Horse":
                        allData = allData.Where(entry => entry.GetType().GetProperty("Type")?.GetValue(entry)?.ToString() == "Horse").ToList();

                        allData = allData.Where(entry =>
                        {
                            var dataProperty = entry.GetType().GetProperty("Data");
                            if (dataProperty != null)
                            {
                                var dataValue = dataProperty.GetValue(entry);
                                if (dataValue is HorseData data && data.Age >= filters.HorseFilter.MinAge && data.Age <= filters.HorseFilter.MaxAge)
                                {
                                    return true;
                                }
                            }
                            return false;
                        }).ToList();

                        if (filters.HorseFilter.Stallion != filters.HorseFilter.Mare)
                        {
                            allData = allData.Where(entry =>
                            {
                                var dataProperty = entry.GetType().GetProperty("Data");
                                if (dataProperty != null)
                                {
                                    var dataValue = dataProperty.GetValue(entry);
                                    if (dataValue is HorseData data && data.Stallion == filters.HorseFilter.Stallion)
                                        return true;
                                }
                                return false;
                            }).ToList();
                        }
                        break;




                    case "User":
                        allData = allData.Where(entry => entry.GetType().GetProperty("Type")?.GetValue(entry)?.ToString() == "User").ToList();
                        if (filters.UserFilter.Private != filters.UserFilter.Public)
                        {
                            allData = allData.Where(entry =>
                            {
                                var dataProperty = entry.GetType().GetProperty("Data");
                                if (dataProperty != null)
                                {
                                    var dataValue = dataProperty.GetValue(entry);
                                    if (dataValue is UserData data && data.isPrivate == filters.UserFilter.Private)
                                        return true;
                                }
                                return false;
                            }).ToList();
                        }
                        break;

                    default:
                        // Handle unknown filter group (optional)
                        break;
                }




                int totalCount = allData.Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / perPage);

                int startIndex = (page - 1) * perPage;

                allData = allData.OrderBy(item =>
                {
                    dynamic d = item;
                    return d.Data.Name.ToString().ToLower();
                }).ToList();

                var search = allData.Skip(startIndex).Take(perPage).ToList();

                return (search, totalPages);
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
                await _context.Users.Include(e => e.UserDetail).ToListAsync(),
                await _context.Jockeys.ToListAsync()
            );

            var (paginatedData, maxPage) = allData.GetPaginatedAllData(page, perPage);

            var result = new
            {
                Search = paginatedData,
                MaxPage = maxPage
            };

            return Ok(result);
        }



        public class JockeyFilter
        {
            public bool Male { get; set; }
            public bool Female { get; set; }
            public bool hashorse { get; set; }
            public bool hasnohorse { get; set; }
        }
        public class HorseFilter
        {
            public int MinAge { get; set; }
            public int MaxAge { get; set; }
            public bool Stallion { get; set; }
            public bool Mare { get; set; }
        }
        public class UserFilter
        {
            public bool Public { get; set; }
            public bool Private { get; set; }
        }

        public class Filters
        {
            public JockeyFilter JockeyFilter { get; set; }
            public UserFilter UserFilter { get; set; }
            public HorseFilter HorseFilter { get; set; }
        }



        [HttpPost]
        public async Task<ActionResult> Post(Filters filters, string filteredGroup, int pageNum, int perPage = 60)
        {
            AllData allData = new AllData(
                await _context.Horses.ToListAsync(),
                await _context.Users.Include(e => e.UserDetail).ToListAsync(),
                await _context.Jockeys.ToListAsync()
            );

            var (paginatedData, maxPage) = allData.GetPaginatedFilteredData(filters, filteredGroup, pageNum, perPage);

            var result = new
            {
                Search = paginatedData,
                MaxPage = maxPage
            };

            return Ok(result);
        }
    }

    internal record HorseData(Guid Id, string? Name, bool Stallion, int Age);

    internal record UserData(Guid Id, string? Name, bool isPrivate);

    internal record JockeyData(Guid Id, string? Name, bool Male, bool hashorse);
}
