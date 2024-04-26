using boredBets.Models;
using boredBets.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace boredBets.Repositories
{
    public class NotificationsService : INotificationsInterface
    {
        private readonly BoredbetsContext _context;

        public NotificationsService(BoredbetsContext context)
        {
            _context = context;
        }

        public async Task<object> GetAllNotificationsByUserId(Guid UserId)
        {
            var notifications = await _context.Notifications.Where(n=>n.UserId==UserId).OrderBy(n => n.Created).ToListAsync();

            if (!notifications.Any())
            {
                return null;
            }

            return notifications;
        }

        public async Task<object> GetAllUnseenNotificationsByUserId(Guid UserId)
        {
            var notifications = await _context.Notifications.Where(n=>n.UserId==UserId && n.Seen == false).OrderBy(n=>n.Created).ToListAsync();

            foreach (var notification in notifications)
            {
                notification.Seen = true;
            }

            await _context.SaveChangesAsync();


            return notifications;
        }
    }
}
