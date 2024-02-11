using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBetController : ControllerBase
    {
        private readonly IUserBetInterface _userBetInterface;

        public UserBetController(IUserBetInterface userBetInterface)
        {
            _userBetInterface = userBetInterface;
        }

        [HttpPost]
        public async Task<ActionResult<UserBet>> Post(Guid Id,Guid HorseId ,Guid RaceId, UserBetCreateDto userBetCreateDto) 
        {
            return StatusCode(201, await _userBetInterface.Post(Id, HorseId,RaceId,userBetCreateDto));
        }
    }
}
