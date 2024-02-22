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

        [HttpPost("UserBetPost")]
        public async Task<ActionResult<UserBet>> Post(Guid Id,Guid HorseId ,Guid RaceId, UserBetCreateDto userBetCreateDto) 
        {
            return StatusCode(201, await _userBetInterface.Post(Id, HorseId,RaceId,userBetCreateDto));
        }

        [HttpGet("GetAllUserBetsByUserId")]
        public async Task<ActionResult<UserBet>> GetAllUserBetByUserId(Guid UserId)
        {
            return StatusCode(201, await _userBetInterface.GetAllUserBetsByUserId(UserId));
        }

        [HttpGet("GetAllUserBetsById")]
        public async Task<ActionResult<UserBet>> GetAllUserBetById(Guid Id)
        {
            return StatusCode(201, await _userBetInterface.GetAllUserBetsById(Id));
        }
    }
}
