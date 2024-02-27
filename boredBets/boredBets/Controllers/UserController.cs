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

        [HttpGet("UserLogin")]
        public async Task<ActionResult<string>> Login(string Email, string Password)
        {
            var result = await userInterface.Login(Email, Password);

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
        public async Task<ActionResult<User>> GetByUserId(Guid id) 
        {
            var result = await userInterface.GetByUserId(id);

            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("GetNewAccessToken")]
        public async Task<ActionResult<User>> GetNewAccessToken(Guid id, string refreshtoken) 
        {
            var result =await userInterface.GetNewAccessToken(id, refreshtoken);

            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        /*[HttpGet("GetByRole")]
        public async Task<ActionResult<User>> GetByRole(string Role)
        {
            if (Role == null) { return BadRequest(); }
            else
            {
                return StatusCode(201, await userInterface.GetByRole(Role));
            }
        }*/


    }
}
