using backapi.Helpers;
using backapi.Helpers.email;
using backapi.Repository;
using backapi.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace backapi.Services
{
    public class EmailService : IEmailRepository
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<globalResponds> SendEmailAsync(EmailRequest request)
        {
            try
            {
                var email = new MimeMessage();

                // From
                email.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));

                // To
                email.To.Add(new MailboxAddress(request.ToName, request.ToEmail));

                // Subject
                email.Subject = request.Subject;

                // Body
                var bodyBuilder = new BodyBuilder();
                if (request.IsHtml)
                {
                    bodyBuilder.HtmlBody = request.Body;
                }
                else
                {
                    bodyBuilder.TextBody = request.Body;
                }

                // Attachments
                foreach (var attachment in request.Attachments)
                {
                    if (File.Exists(attachment))
                    {
                        bodyBuilder.Attachments.Add(attachment);
                    }
                }

                email.Body = bodyBuilder.ToMessageBody();

                // Send email
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return new globalResponds("1", "Email sent successfully", new { ToEmail = request.ToEmail, Subject = request.Subject });


            }
            catch (Exception ex)
            {
                return new globalResponds("0", "Email sent fail", null);
            }
        }

        public async Task<globalResponds> SendWelcomeEmailAsync(string email, string name)
        {
            var htmlBody = $@"
                <html>
                <body>
                    <h1>Welcome {name}!</h1>
                    <p>Thank you for registering with our service.</p>
                    <p>We're excited to have you on board!</p>
                    <br>
                    <p>Best regards,<br>Your App Team</p>
                </body>
                </html>";

            var emailRequest = new EmailRequest
            {
                ToEmail = email,
                ToName = name,
                Subject = "Welcome to Our Service!",
                Body = htmlBody,
                IsHtml = true
            };

            return await SendEmailAsync(emailRequest);
        }

        public async Task<globalResponds> SendPasswordResetEmailAsync(string email, string resetToken)
        {
            var resetUrl = $"https://yourapp.com/reset-password?token={resetToken}";

            var htmlBody = $@"
                <html>
                <body>
                    <h1>Password Reset Request</h1>
                    <p>You have requested to reset your password.</p>
                    <p>Click the link below to reset your password:</p>
                    <a href='{resetUrl}' style='background-color: #4CAF50; color: white; padding: 14px 20px; text-decoration: none; display: inline-block;'>Reset Password</a>
                    <p>If you didn't request this, please ignore this email.</p>
                    <p>This link will expire in 1 hour.</p>
                </body>
                </html>";

            var emailRequest = new EmailRequest
            {
                ToEmail = email,
                ToName = email,
                Subject = "Password Reset Request",
                Body = htmlBody,
                IsHtml = true
            };

            return await SendEmailAsync(emailRequest);
        }

        public async Task<globalResponds> SendVerificationEmailAsync(string email, string verificationToken)
        {
            var verificationUrl = $"https://yourapp.com/verify-email?token={verificationToken}";

            var htmlBody = $@"
                <html>
                <body>
                    <h1>Email Verification</h1>
                    <p>Please verify your email address by clicking the link below:</p>
                    <a href='{verificationUrl}' style='background-color: #008CBA; color: white; padding: 14px 20px; text-decoration: none; display: inline-block;'>Verify Email</a>
                    <p>If you didn't create this account, please ignore this email.</p>
                </body>
                </html>";

            var emailRequest = new EmailRequest
            {
                ToEmail = email,
                ToName = email,
                Subject = "Please Verify Your Email",
                Body = htmlBody,
                IsHtml = true
            };

            return await SendEmailAsync(emailRequest);
        }
    }
}
