using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public class SendGridEmailSender : IEmailSender<ApplicationUser>
    {
        // TODO:
        // Update when it's time to set up email confirmation
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        Task.CompletedTask;

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            Task.CompletedTask;

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            Task.CompletedTask;
    }
}
