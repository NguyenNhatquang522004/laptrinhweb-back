using backapi.Helpers;


namespace backapi.Repository
{
    public interface IEmailRepository
    {
        Task<globalResponds> SendEmailAsync(Helpers.email.EmailRequest request);
        Task<globalResponds> SendWelcomeEmailAsync(string email, string name);
        Task<globalResponds> SendPasswordResetEmailAsync(string email, string resetToken);
        Task<globalResponds> SendVerificationEmailAsync(string email, string verificationToken);

    }
}
