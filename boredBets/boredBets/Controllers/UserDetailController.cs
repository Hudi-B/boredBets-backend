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
            if (result == "0") 
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAllTransactionsByUserId")]
        public async Task<ActionResult<string>> GetAllTransactionsByUserId(Guid UserId) 
        {
            var result = await _userDetail.GetAllTransactionsByUserId(UserId);
            if (result.Count() == 0) 
            {
                return "No transaction found";
            }
            return Ok(result);
        }

        [HttpGet("GetIsPrivateByUserId")]
        public async Task<ActionResult<bool>> GetIsPrivateByUserId(Guid UserId) 
        {
            return await _userDetail.GetIsPrivateByUserId(UserId);
        }

        [HttpPut("UpdateUserDetailByUserId")]
        public async Task<ActionResult<UserDetail>> UpdateUserDetailByUserId(Guid UserId, UserDetailUpdateDto updateDto)
        {
            var result = await _userDetail.UpdateUserDetailByUserId(UserId, updateDto);
            if(result == null) 
            { 
                return  NotFound(); 
            }
            return Ok(result);
        }

        [HttpPut("Preferences")]
        public async Task<ActionResult<UserDetail>> Preferences(Guid UserId, bool IsPrivate)
        {
            var result = await _userDetail.Preferences(UserId, IsPrivate);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
