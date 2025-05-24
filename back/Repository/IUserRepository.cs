using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IUserRepository
    {

        Task<globalResponds> GetUserByIdAsync(Guid userId);
        Task<globalResponds> GetUserByEmailAsync(string email);
        Task<globalResponds> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<globalResponds> CreateUserAsync(User user);
        Task<globalResponds> UpdateUserAsync(User user);
        Task<globalResponds> DeleteUserAsync(Guid userId);
        Task<globalResponds> UserExistsAsync(Guid userId);
        Task<globalResponds> SaveChangesAsync();
    }
}
