using boredBets.Models;
using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailInterface _userDetail;

        public UserDetailController(IUserDetailInterface userDetail)
        {
            _userDetail = userDetail;
        }

        [HttpPost("UserDetailPost")]
        public async Task<ActionResult<UserDetail>> Post(Guid Id, UserDetailCreateDto userDetailCreateDto) 
        {
            var result = await _userDetail.Post(Id, userDetailCreateDto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetUserDetailByUserId")]
        public async Task<ActionResult<UserDetail>> GetUserDetailByUserId(Guid UserId)
        {
            var result = await _userDetail.GetUserDetailByUserId(UserId);
            if (result == null) 
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
