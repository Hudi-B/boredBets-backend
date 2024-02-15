using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
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
        public async Task<ActionResult<User>> Post(UserCreateDto userCreateDto) 
        {
            if (userCreateDto == null) { return BadRequest(); }
            else 
            { 
                return StatusCode(201, await userInterface.Post(userCreateDto)); 
            }
        }
        [HttpGet("UserLogin")]
        public async Task<ActionResult<string>> Get(string Email, string Password)
        {
            return StatusCode(201, await userInterface.Get(Email, Password));
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

        
    }
}
