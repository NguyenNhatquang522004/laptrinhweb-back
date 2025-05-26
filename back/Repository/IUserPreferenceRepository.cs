using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IUserPreferenceRepository
    {
        public Task<globalResponds> GetUserPreferenceAsync(Guid userId);
        public Task<globalResponds> CreateUserPreferenceAsync(UserPreference userPreference);
        public Task<globalResponds> UpdateUserPreferenceAsync(UserPreference userPreference);
        public Task<globalResponds> DeleteUserPreferenceAsync(Guid userId);

    }
}
