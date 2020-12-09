using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoWish.Models;

namespace TodoWish.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly Services.IMailService mailService;

        public MailController(Services.IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}