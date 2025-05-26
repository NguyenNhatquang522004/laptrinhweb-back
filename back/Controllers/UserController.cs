using backapi.Helpers;
using backapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backapi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class userController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        public userController(IUserRepository userRepository, IUserPreferenceRepository userPreferenceRepository)
        {
            _userRepository = userRepository;
            _userPreferenceRepository = userPreferenceRepository;
        }

        [HttpDelete]
        public IActionResult test()
        {

            return Ok(new globalResponds("200", "Google login successful.", null));
        }

    }
}
