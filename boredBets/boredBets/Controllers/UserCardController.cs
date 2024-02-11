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

        [HttpPost]
        public async Task<ActionResult<UserCard>> Post(Guid Id, UserCardCreateDto userCardCreateDto)
        {
            if (userCardCreateDto.CreditcardNum == null)
            {
                return BadRequest("Credit card number cannot be null");
            }

            var result = await _userCardInterface.Post(Id, userCardCreateDto);

            if (result == null)
            {
                return BadRequest("User not found or credit card already exists");
            }

            return StatusCode(201, result);
        }

    }
}
