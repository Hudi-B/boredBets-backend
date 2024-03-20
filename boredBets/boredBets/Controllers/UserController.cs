using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface userInterface;

        public UserController(IUserInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        [HttpPost("UserRegister")]
        public async Task<ActionResult<User>> Register(UserCreateDto userCreateDto)
        {
            var result = await userInterface.Register(userCreateDto);

            if (result==null)
            {
                return Conflict();
            }
            return Ok(result);
        }

        [HttpPost("UserLogin")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
            var result = await userInterface.Login(userLoginDto);

            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<User>> GetAllUser() 
        {
            var result = await userInterface.GetAllUser();

            if (result == null)
            { 
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("GetByUserId")]
        public async Task<ActionResult<User>> GetByUserId(Guid UserId) 
        {
            var result = await userInterface.GetByUserId(UserId);

            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("GetNewAccessToken")]
        public async Task<ActionResult<User>> GetNewAccessToken(Guid UserId, string refreshtoken) 
        {
            var result =await userInterface.GetNewAccessToken(UserId, refreshtoken);

            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("GetWalletByUserId")]
        public async Task<ActionResult<User>> GetWalletByUserId(Guid UserId)
        {
            var result = await userInterface.GetWalletByUserId(UserId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("UpdateWalletByUserId")]
        public async Task<ActionResult<UserWalletDto>> UpdateWalletByUserId(Guid UserId, UserWalletDto wallet)
        {
            var result = await userInterface.UpdateWalletByUserId(UserId,wallet);

            if (result == null)
            {
                return NotFound("User not found or value was lower than initial value");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteUserById")]
        public async Task<ActionResult<User>> DeleteUserById(Guid UserId)
        {
            var result = await userInterface.DeleteUserById(UserId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("UpdateUsernameByUserId")]
        public async Task<ActionResult<User>> UpdateUsernameByUserId(Guid UserId, UsernameDto username)
        {
            var result = await userInterface.UpdateUsernameByUserId(UserId,username);

            if (result == "0")
            {
               return NoContent();
            }
            else if (result== "1")
            {
               return Conflict("Username already exists");
            }

            return Ok();
        }
    }
}
