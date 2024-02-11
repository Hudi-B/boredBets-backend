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

        [HttpPost]
        public async Task<ActionResult<User>> Post(UserCreateDto userCreateDto) 
        {
            if (userCreateDto == null) { return BadRequest(); }
            else 
            { 
                return StatusCode(201, await userInterface.Post(userCreateDto)); 
            }
        }
    }
}
