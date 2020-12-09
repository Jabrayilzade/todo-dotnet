using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWish.Models;

namespace TodoWish.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
