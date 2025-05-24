using backapi.Model;
using System.Security.Claims;

namespace backapi.Repository
{
    public interface IJwtRepository
    {
        string GenerateToken(User user);
        ClaimsPrincipal ValidateToken(string token);
    }
}
