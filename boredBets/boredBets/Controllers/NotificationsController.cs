using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsInterface _notifications;

        public NotificationsController(INotificationsInterface notifications)
        {
            _notifications = notifications;
        }

        [HttpGet("GetAllNotificationsByUserId")]
        public async Task<ActionResult> GetAllNotificationsByUserId(Guid UserId) 
        {
            var result = await _notifications.GetAllNotificationsByUserId(UserId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAllUnseenNotificationsByUserId")]
        public async Task<ActionResult> GetAllUnseenNotificationsByUserId(Guid UserId)
        {
            var result = await _notifications.GetAllUnseenNotificationsByUserId(UserId);
            if (result == null)
            {
                return  NotFound();
            }
            return Ok(result);
        }
    }
}
