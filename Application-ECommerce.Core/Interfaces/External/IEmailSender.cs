using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Interfaces.External
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailMessage);
        Task SendWelcomeEmailAsync(string email, string username);
        Task SendPasswordResetEmailAsync(string email, string resetToken, string userId);
        Task SendEmailConfirmationAsync(string email, string confirmationToken, string userId);

    }
}
