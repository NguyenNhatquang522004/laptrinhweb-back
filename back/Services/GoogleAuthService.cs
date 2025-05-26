using backapi.auth;
using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using backapi.Settings;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backapi.Services
{
    public class GoogleAuthService : IGoogleAuthRepository
    {
        private readonly GoogleAuthSettings _googleSettings;
        private readonly ApplicationDbContext _context;
        private readonly IJwtRepository _jwtService;

        public GoogleAuthService(
            IOptions<GoogleAuthSettings> googleSettings,
            ApplicationDbContext context,
            IJwtRepository jwtService)
        {
            _googleSettings = googleSettings.Value;
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<globalResponds> AuthenticateGoogleUserAsync(string idToken)
        {
            try
            {
                // Verify Google ID Token
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _googleSettings.ClientId }
                });

                // Check if user exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == payload.Email);

                User user;

                if (existingUser == null)
                {
                    // Create new user
                    user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Email = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        ProfileImageUrl = payload.Picture,
                        prvider = "Google",
                        providerId = payload.Subject,
                        IsActive = true,
                        EmailVerified = payload.EmailVerified,
                        PasswordHash = null,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Update existing user info
                    existingUser.FirstName = payload.GivenName;
                    existingUser.LastName = payload.FamilyName;
                    existingUser.ProfileImageUrl = payload.Picture;
                    existingUser.EmailVerified = payload.EmailVerified;


                    if (string.IsNullOrEmpty(existingUser.providerId))
                    {
                        existingUser.providerId = payload.Subject;
                        existingUser.prvider = "Google";
                    }

                    await _context.SaveChangesAsync();
                    user = existingUser;
                }

                // Generate JWT token
                var accessToken = _jwtService.GenerateToken(user);

                var authResponse = new AuthResponse
                {
                    AccessToken = accessToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                    User = new UserInfo
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Picture = user.ProfileImageUrl,
                        Provider = user.prvider
                    }
                };
                return new globalResponds("200", "Google authentication successful.", authResponse);
            }
            catch (InvalidJwtException)
            {
                return new globalResponds("401", "Invalid Google ID token.", null);
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết để debug
                Console.WriteLine($"Google Auth Error: {ex.Message}");
                return new globalResponds("500", "Google authentication failed.", null);
            }
        }
    }
}

