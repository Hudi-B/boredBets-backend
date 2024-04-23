using boredBets.Repositories.Interface;

namespace boredBets.Repositories
{
    public class NotificationsService : INotificationsInterface
    {
        public Task<object> GetAllNotificationsByUserId(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllUnseenNotificationsByUserId(Guid UserId)
        {
            throw new NotImplementedException();
        }
    }
}
