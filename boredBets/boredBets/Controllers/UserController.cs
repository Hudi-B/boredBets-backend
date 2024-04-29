using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

            switch (result)
            {
                case "0":
                    return Conflict("User with this email already exists");
                    
                case "1":
                    return Conflict("User with this Username already exists");
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
            else if (result.GetType() == typeof(Guid))
            {
                return Problem("User not verified%"+result);
            }
            return Ok(result);
        }

        [HttpPost("UserLoginByRefreshToken")]
        public async Task<ActionResult<object>> UserLoginByRefreshToken(string RefreshToken)
        {
            var result = await userInterface.UserLoginByRefreshToken(RefreshToken);

            if (result == "0")
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("VerificationCodeCheck")]
        public async Task<ActionResult> VerificationCodeCheck(Guid UserId, string VerificationCode) 
        {
            var result = await userInterface.VerificationCodeCheck(UserId,VerificationCode);

            if (result == null)
            {
                return NotFound("User not found");
            }
            else if(result == "1") 
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
        [HttpGet("GetUserDetailsByUserId")]
        public async Task<object> GetUserDetailsByUserId(Guid UserId) 
        {
            var result = await userInterface.GetUserDetailsByUserId(UserId);

            if (result == "0") 
            {
                return NotFound("User not found");
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

            if (result == "0")
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }

        [HttpPut("UpdateEmailByUserId")]
        public async Task<ActionResult<string>> UpdateEmailByUserId(Guid UserId,UserEmailDto emailDto) 
        {
            var result = await userInterface.UpdateEmailByUserId(UserId, emailDto);

            if (result == "0")
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }

        [HttpPut("UpdatePasswordByUserId")]
        public async Task<ActionResult<User>> UpdatePasswordByUserId(Guid UserId, UserUpdatebyUserIdPassword passwordDto)
        {
            var result = await userInterface.UpdatePasswordByUserId(UserId, passwordDto);
            if (result == "0")
            {
                return NotFound("User not found");
            }
            return Ok();
        }

        [HttpPut("UpdatePasswordByOldPassword")]
        public async Task<ActionResult<User>> UpadatePasswordByOldPassword(Guid UserId, UserPasswordDto passwordDto)
        {
            var result = await userInterface.UpadatePasswordByOldPassword(UserId, passwordDto);
            if (result == "0")
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPut("UpdateUsernameByUserId")]
        public async Task<ActionResult<User>> UpdateUsernameByUserId(Guid UserId, UsernameDto username)
        {
            var result = await userInterface.UpdateUsernameByUserId(UserId, username);

            if (result == "0")
            {
                return NoContent();
            }
            else if (result == "1")
            {
                return Conflict("Username already exists");
            }

            return Ok();
        }

        [HttpPut("UpdateImageByUserId")]
        public async Task<ActionResult<User>> UpdateImageByUserId(Guid UserId, ImageUpdateByUserId imageUpdate)
        {
            var result = await userInterface.UpdateImageByUserId(UserId, imageUpdate);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("DeleteImageByUserId")]
        public async Task<ActionResult<User>> DeleteImageByUserId(Guid UserId, ImageUpdateByUserId imageUpdate)
        {
            var result = await userInterface.UpdateImageByUserId(UserId, imageUpdate);

            if (result == null || result=="1")
            {
                return NotFound();
            }

            return Ok();
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

        [HttpPost("ForgotYourPassword")]
        public async Task<ActionResult<string>> ForgotYourPassword(string Email)
        {
            var result = await userInterface.ForgotYourPassword(Email);

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Bonus")]
        public async Task<ActionResult<bool>> Bonus(Guid UserId)
        {
            var result = await userInterface.Bonus(UserId);

            if (result == null)
            {
                return false;
            }

            return Ok(true);
        }
    }
}
