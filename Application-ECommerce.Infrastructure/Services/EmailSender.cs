using System.Threading.Tasks;
using Application_ECommerce.Core.Interfaces.External;

namespace Application_ECommerce.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string emailMessage)
        {
            // TODO: Implement email sending logic
            return Task.CompletedTask;
        }

        public Task SendEmailConfirmationAsync(string email, string confirmationToken, string userId)
        {
            // TODO: Implement email sending logic
            return Task.CompletedTask;
        }

        public Task SendPasswordResetEmailAsync(string email, string resetToken, string userId)
        {
            // TODO: Implement email sending logic
            return Task.CompletedTask;
        }

        public Task SendWelcomeEmailAsync(string email, string username)
        {
            // TODO: Implement email sending logic
            return Task.CompletedTask;
        }
    }
}
