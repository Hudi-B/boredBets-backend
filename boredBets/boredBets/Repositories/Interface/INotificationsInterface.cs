namespace boredBets.Repositories.Interface
{
    public interface INotificationsInterface
    {
        Task<object> GetAllUnseenNotificationsByUserId(Guid UserId);
        Task<object> GetAllNotificationsByUserId(Guid UserId);
    }
}
