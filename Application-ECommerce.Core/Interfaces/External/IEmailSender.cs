using Application_ECommerce.Core.Not_Mapped_Entities;

namespace Application_ECommerce.Core.Interfaces.External
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage emailMessage);
        Task SendWelcomeEmailAsync(string email, string username);
        Task SendPasswordResetEmailAsync(string email, string resetToken, string userId);
        Task SendEmailConfirmationAsync(string email, string confirmationToken, string userId);
    }
}
