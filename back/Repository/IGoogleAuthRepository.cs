using backapi.Helpers;

namespace backapi.Repository
{
    public interface IGoogleAuthRepository
    {
        Task<globalResponds> AuthenticateGoogleUserAsync(string idToken);
    }
}
