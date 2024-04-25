using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace boredBets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailInterface emailInterface;

        public EmailController(IEmailInterface emailInterface)
        {
            this.emailInterface = emailInterface;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDTO request)
        {
            emailInterface.SendEmail(request);
            return StatusCode(200, "Email sent.");
        }
    }
}
