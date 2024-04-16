using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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

            if (result == "0")
            {
                return Unauthorized("User already has a bet on this race");
            }
            else if (result == "1")
            {
                return NotFound("One of the horses isn't in the race");
            }
            else if (result == "2")
            {
                return NoContent();
            }
            else if (result == "3")
            {
                return Forbid("Not enough money to bet");
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
