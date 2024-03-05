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
        public async Task<ActionResult<UserBet>> Post(UserBetCreateDto userBetCreateDto) 
        {
            var result = await _userBetInterface.Post(userBetCreateDto);

            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAllUserBetsByUserId")]
        public async Task<ActionResult<UserBet>> GetAllUserBetByUserId(Guid UserId)
        {
            var result = await _userBetInterface.GetAllUserBetsByUserId(UserId);

            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetUserBetsById")]
        public async Task<ActionResult<UserBet>> GetUserBetById(Guid Id)
        {
            var result = await _userBetInterface.GetUserBetsById(Id);

            if (result==null) 
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteUserBetById")]
        public async Task<ActionResult<UserBet>> DeleteUserBetById(Guid Id) 
        {
            var result = await _userBetInterface.DeleteUserBetById(Id);

            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
