using backapi.Configuration;
using backapi.DTO;
using backapi.Helpers;
using backapi.Helpers.email;
using backapi.Model;
using backapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backapi.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly passwordHepler passwordHepler;
        private readonly IEmailRepository _emailService;
        public RegisterController(IUserRepository userRepository, ApplicationDbContext applicationDbContext, passwordHepler passwordHepler, IEmailRepository emailService)
        {
            _userRepository = userRepository;
            this.applicationDbContext = applicationDbContext;
            this.passwordHepler = passwordHepler;
            _emailService = emailService;
        }





        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest(new globalResponds("400", "Email already exists.", null));
                }
                user.PasswordHash = passwordHepler.HashPassword(user.PasswordHash);
                var result = await _userRepository.CreateUserAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new globalResponds("500", "An error occurred while registering the user.", null));
            }
        }

        [HttpPost]
        [Route("stepone")]
        public async Task<IActionResult> RegisterStepOne([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                globalResponds searhUser = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
                if (searhUser.Code == "0" || searhUser.Data == null)
                {
                    return BadRequest(new globalResponds("400", "Email already exists.", null));
                }
                Guid guid = Guid.NewGuid();
                string handleguid = guid.ToString().Replace("-", "").Substring(0, 6); // Generate a 6-character code
                User newUser = new User
                {
                    Email = registerDTO.Email,
                    code = handleguid,
                    CodeExpiration = DateTime.UtcNow.AddMinutes(30),
                };
                globalResponds sendmail = await _emailService.SendEmailAsync(new EmailRequest
                {
                    ToEmail = registerDTO.Email,
                    Subject = "Verify your email",
                    Body = $"Your verification code is: {handleguid}",
                    IsHtml = true,
                });
                if (sendmail.Code != "1")
                {
                    return StatusCode(500, new globalResponds("500", "Failed to send verification email.", null));
                }
                globalResponds createUserResponse = await _userRepository.CreateUserAsync(newUser);
                if (createUserResponse.Code != "1")
                {
                    return StatusCode(500, new globalResponds("500", "Failed to create user.", null));
                }
                return StatusCode(200, new globalResponds("200", "Register step one successfull.", newUser.Email));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new globalResponds("500", "An error occurred while registering the user.", null));
            }

        }

        [HttpPost]
        [Route("steptwo")]
        public async Task<IActionResult> RegisterStepTwo([FromBody] RegisterDTO request)
        {
            try
            {
                globalResponds searchUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (searchUser.Code == "0" || searchUser.Data == null)
                {
                    return BadRequest(new globalResponds("400", "Email not found.", null));
                }


                User user = (User)searchUser.Data;


                if (user.code != request.Code || user.CodeExpiration < DateTime.UtcNow)
                {
                    return BadRequest(new globalResponds("400", "Invalid or expired verification code.", null));
                }
                user.code = string.Empty; // Clear the code after successful verification
                user.CodeExpiration = DateTime.MinValue; // Clear the expiration date
                user.IsActive = true; // Activate the user
                user.prvider = "Local"; // Set provider to Local
                globalResponds updateUser = await _userRepository.UpdateUserAsync(user); // Update the user in the repository
                if (updateUser.Code != "1")
                {
                    return StatusCode(500, new globalResponds("500", "Failed to update user.", null));
                }

                return StatusCode(200, new globalResponds("200", "Register step two successfull.", user.Email));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new globalResponds("500", "An error occurred while registering the user.", null));

            }
        }

        [HttpPost]
        [Route("stepthree")]
        public async Task<IActionResult> RegisterStepThree([FromBody] RegisterDTO request)
        {
            try
            {
                globalResponds searchUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (searchUser.Code == "0" || searchUser.Data == null)
                {
                    return BadRequest(new globalResponds("400", "Email not found.", null));
                }
                string hashedPassword = passwordHepler.HashPassword(request.PasswordHash);
                if (hashedPassword == null)
                {
                    return BadRequest(new globalResponds("400", "Invalid password.", null));
                }
                User user = (User)searchUser.Data;
                user.PasswordHash = hashedPassword; // Set the hashed password
                user.FirstName = request.FirstName; // Set the first name
                user.LastName = request.LastName; // Set the last name
                user.Username = request.Username; // Set the username
                user.DateOfBirth = request.DateOfBirth; // Set the date of birth
                globalResponds updateUser = await _userRepository.UpdateUserAsync(user); // Update the user in the repository
                if (updateUser.Code != "1")
                {
                    return StatusCode(500, new globalResponds("500", "Failed to update user.", null));
                }

                return StatusCode(200, new globalResponds("200", "Register step three successfull.", user.Email));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new globalResponds("500", "An error occurred while registering the user.", null));
            }


        }


        [HttpPost]
        [Route("stepfour")]
        public async Task<globalResponds> RegisterStepFour([FromBody] RegisterDTO request)
        {
            try
            {
                globalResponds searchUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (searchUser.Code == "0" || searchUser.Data == null)
                {
                    return new globalResponds("400", "Email not found.", null);
                }
                User user = (User)searchUser.Data;
                user.NativeLanguage = request.NativeLanguage; // Set the native language
                user.Country = request.Country; // Set the country
                user.Timezone = request.Timezone; // Set the timezone
                user.ProfileImageUrl = request.ProfileImageUrl; // Set the profile image URL
                user.Bio = request.Bio; // Set the bio
                user.IsPremium = request.IsPremium; // Set the premium status
                user.CurrentLevel = request.CurrentLevel; // Set the current level
                globalResponds updateUser = await _userRepository.UpdateUserAsync(user); // Update the user in the repository
                if (updateUser.Code != "1")
                {
                    return new globalResponds("500", "Failed to update user.", null);
                }
                globalResponds sendmail = await _emailService.SendWelcomeEmailAsync(user.Email, user.Username);
                if (sendmail.Code != "1")
                {
                    return new globalResponds("500", "Failed to send welcome email.", null);
                }
                return new globalResponds("200", "Register step four successfull.", user.Email);
            }
            catch (Exception ex)
            {
                return new globalResponds("500", "An error occurred while registering the user.", null);
            }
        }
    }
}
