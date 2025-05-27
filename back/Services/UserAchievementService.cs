using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class UserAchievementService : IUserAchievementRepository
    {
        private readonly ApplicationDbContext _context;
        public UserAchievementService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<globalResponds> Create(UserAchievement userAchievement, Achievement achievement, User user)
        {
            try
            {
                userAchievement.UserId = user.UserId;
                userAchievement.AchievementId = achievement.AchievementId;
                userAchievement.Achievement = achievement;
                userAchievement.User = user;
                achievement.UserAchievements.Add(userAchievement);
                user.UserAchievements.Add(userAchievement);
                _context.UserAchievements.Add(userAchievement);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> Delete(UserAchievement userAchievement)
        {
            try
            {
                userAchievement.Achievement.UserAchievements.Remove(userAchievement);
                userAchievement.User.UserAchievements.Remove(userAchievement);
                userAchievement.Achievement = null;
                userAchievement.User = null;
                _context.UserAchievements.Remove(userAchievement);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetAll()
        {
            try
            {
                List<UserAchievement> list = await _context.UserAchievements.ToListAsync();
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetById(Guid id)
        {
            try
            {
                UserAchievement list = await _context.UserAchievements.FindAsync(id);
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> Update(UserAchievement userAchievement, Guid id)
        {
            try
            {
                UserAchievement list = await _context.UserAchievements.FindAsync(id);
                if (list == null)
                {
                    return new globalResponds("0", "không tìm thấy người dùng thành tích", null);
                }
                _context.Entry(list).CurrentValues.SetValues(userAchievement);
                _context.UserAchievements.Update(list);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }
    }
}
