using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class userPreferenceService : IUserPreferenceRepository
    {

        private readonly ApplicationDbContext applicationDbContext;
        public userPreferenceService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<globalResponds> GetUserPreferenceAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return new globalResponds("400", "User ID không hợp lệ.", null);
                }
                UserPreference searchUserPreference = await applicationDbContext.UserPreferences.FirstOrDefaultAsync(up => up.UserId == userId);
                return new globalResponds("1", "thành công.", searchUserPreference);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "Đã xảy ra lỗi khi lấy thông tin sở thích người dùng.", null);
            }
        }
        public async Task<globalResponds> CreateUserPreferenceAsync(UserPreference request)
        {
            // Implementation for creating user preferences
            try
            {

                UserPreference userPreference = new UserPreference();
                userPreference.UserId = request.UserId;
                userPreference.User = request.User;
                applicationDbContext.UserPreferences.Add(userPreference);
                await applicationDbContext.SaveChangesAsync();
                return new globalResponds("1", "thành công.", userPreference);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "Đã xảy ra lỗi khi lấy thông tin sở thích người dùng.", null);
            }
        }
        public async Task<globalResponds> UpdateUserPreferenceAsync(UserPreference request)
        {
            try
            {
                var update = await applicationDbContext.UserPreferences.FirstOrDefaultAsync(up => up.UserId == request.UserId);
                if (update == null)
                {
                    return new globalResponds("404", "Sở thích người dùng không tồn tại.", null);
                }
                applicationDbContext.Entry(update).CurrentValues.SetValues(request);
                applicationDbContext.UserPreferences.Update(update);
                await applicationDbContext.SaveChangesAsync();
                return new globalResponds("1", "thành công.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "Đã xảy ra lỗi khi lấy thông tin sở thích người dùng.", null);
            }
        }
        public async Task<globalResponds> DeleteUserPreferenceAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return new globalResponds("400", "User ID không hợp lệ.", null);
                }
                UserPreference searchUserPreference = await applicationDbContext.UserPreferences.FirstOrDefaultAsync(up => up.UserId == userId);
                applicationDbContext.UserPreferences.Remove(searchUserPreference);
                await applicationDbContext.SaveChangesAsync();

                return new globalResponds("1", "thành công.", searchUserPreference);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "Đã xảy ra lỗi khi lấy thông tin sở thích người dùng.", null);
            }
        }
    }
}
