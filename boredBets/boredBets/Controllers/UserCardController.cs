using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardController : ControllerBase
    {
        private readonly IUserCardInterface _userCardInterface;

        public UserCardController(IUserCardInterface userCardInterface)
        {
            _userCardInterface = userCardInterface;
        }

        [HttpPost("UserCardPost")]
        public async Task<ActionResult<UserCard>> Post(UserCardCreateDto userCardCreateDto)
        {

            var result = await _userCardInterface.Post(userCardCreateDto);

            if (result == null)
            {
                return BadRequest("User not found or credit card already exists");
            }

            return StatusCode(201, result);
        }

        [HttpGet("GetAllUserCardsByUserId")]
        public async Task<ActionResult<UserCard>> GetAllUserCardsByUserId(Guid UserId) 
        {
            var result = await _userCardInterface.GetAllUserCardsByUserId(UserId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteByCreditCardNum")]
        public async Task<ActionResult<UserCard>> DeleteByCreditCardNum(string CreditCardNum)
        {
            var result = await _userCardInterface.DeleteByCreditCardNum(CreditCardNum);

            if (result == null)
            {
                NotFound();
            }

            return Ok(result);
        }

    }
}
