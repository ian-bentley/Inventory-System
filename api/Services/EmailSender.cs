using System.Net.Mail;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _domainEmail = "ibentley981203@gmail.com";

        public EmailSender(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(_domainEmail, email, subject, message);
            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            var subject = "Confirm Your Email Address";
            var message = $"Hello {user.UserName},\n\nPlease confirm your email address by clicking the following link: {confirmationLink}";

            var mailMessage = new MailMessage("noreply@yourdomain.com", email, subject, message);
            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            var subject = "Password Reset Code";
            var message = $"Hello {user.UserName},\n\nYour password reset code is: {resetCode}\n\nUse this code to reset your password.";

            var mailMessage = new MailMessage("noreply@yourdomain.com", email, subject, message);
            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            var subject = "Reset Your Password";
            var message = $"Hello {user.UserName},\n\nPlease reset your password by clicking the following link: {resetLink}";

            var mailMessage = new MailMessage("noreply@yourdomain.com", email, subject, message);
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
