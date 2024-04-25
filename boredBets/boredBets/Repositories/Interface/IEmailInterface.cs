using boredBets.Models.Dtos;

namespace boredBets.Repositories.Interface
{
    public interface IEmailInterface
    {
        void SendEmail(EmailDTO request);
    }
}
