using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
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
            if (userCreateDto == null) { return BadRequest(); }
            else
            {
                return StatusCode(201, await userInterface.Register(userCreateDto));
            }
        }

        [HttpGet("UserLogin")]
        public async Task<ActionResult<string>> Login(string Email, string Password)
        {
            return StatusCode(201, await userInterface.Login(Email, Password));
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<User>> GetAllUser() 
        {
            return StatusCode(201, await userInterface.GetAllUser());
        }

        [HttpGet("GetByEmail")]
        public async Task<ActionResult<User>> GetByEmail(string Email) 
        {
            if (Email == null) { return BadRequest(); }
            else
            {
                return StatusCode(201, await userInterface.GetByEmail(Email));
            }
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
