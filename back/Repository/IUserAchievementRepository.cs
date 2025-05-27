using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IUserAchievementRepository
    {
        public Task<globalResponds> GetAll();
        public Task<globalResponds> Delete(UserAchievement userAchievement);
        public Task<globalResponds> Update(UserAchievement userAchievement, Guid id);
        public Task<globalResponds> GetById(Guid id);
        public Task<globalResponds> Create(UserAchievement userAchievement, Achievement achievement, User user);

    }
}
