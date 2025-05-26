using backapi.auth;
using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backapi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly passwordHepler passwordHepler;
        private readonly IJwtRepository jwtRepository;
        private readonly IGoogleAuthRepository _googleAuthService;
        public LoginController(IUserRepository userRepository, ApplicationDbContext applicationDbContext, passwordHepler passwordHepler, IJwtRepository jwtRepository)
        {
            _userRepository = userRepository;
            this.applicationDbContext = applicationDbContext;
            this.passwordHepler = passwordHepler;
            this.jwtRepository = jwtRepository;
        }

        [HttpPost]
        [Route("local")]
        public async Task<IActionResult> LocalLogin([FromBody] User request)
        {
            try
            {
                globalResponds existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

                if (existingUser.Data == null)
                {
                    return BadRequest(new globalResponds("400", "Email does not exist.", null));
                }
                User user = (User)existingUser.Data;
                if (!passwordHepler.VerifyPassword(user.PasswordHash, user.PasswordHash))
                {
                    return Unauthorized(new globalResponds("401", "Invalid password.", null));
                }
                if (user.Email != request.Email)
                {
                    return BadRequest(new globalResponds("400", "Email not verified.", null));
                }

                return Ok(new globalResponds("200", "Login successful.", existingUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new globalResponds("500", "An error occurred while logging in.", null));
            }
        }

        [HttpPost]
        [Route("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.IdToken))
                {
                    return BadRequest(new globalResponds("400", "Google ID token is required.", null));
                }

                // Sử dụng GoogleAuthService để xác thực
                var result = await _googleAuthService.AuthenticateGoogleUserAsync(request.IdToken);

                if (result.Data == null)
                {
                    return Unauthorized(new globalResponds("401", "Invalid Google token or authentication failed.", null));
                }

                return Ok(new globalResponds("200", "Google login successful.", result.Data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new globalResponds("500", "An error occurred during Google authentication.", null));
            }
        }

    }
}
