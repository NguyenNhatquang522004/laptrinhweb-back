using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class UserChallengeService : IUserChallengeRepository
    {
        private readonly ApplicationDbContext _context;


        public UserChallengeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds> CreateUserChallengeAsync(UserChallenge userChallenge, User user, Challenge challenge)
        {
            try
            {

                user.UserChallenges.Add(userChallenge);
                challenge.UserChallenges.Add(userChallenge);
                userChallenge.ChallengeId = challenge.ChallengeId;
                userChallenge.UserId = user.UserId;
                _context.UserChallenges.Add(userChallenge);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteUserChallengeAsync(UserChallenge userChallengeId)
        {
            try
            {
                userChallengeId.User.UserChallenges.Remove(userChallengeId);
                userChallengeId.Challenge.UserChallenges.Remove(userChallengeId);
                userChallengeId.User = null;
                userChallengeId.Challenge = null;
                _context.UserChallenges.Remove(userChallengeId);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetAllUserChallenges()
        {
            try
            {
                List<UserChallenge> list = await _context.UserChallenges.ToListAsync();
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetUserChallengeByIdAsync(Guid userChallengeId)
        {
            try
            {
                UserChallenge list = await _context.UserChallenges.FindAsync(userChallengeId);
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetUserChallengesByChallengeIdAsync(Guid challengeId)
        {
            try
            {
                UserChallenge list = await _context.UserChallenges.FindAsync(challengeId);
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetUserChallengesByUserIdAsync(Guid userId)
        {
            try
            {
                List<UserChallenge> list = await _context.UserChallenges.Where(o => o.UserId == userId).ToListAsync();
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> UpdateUserChallengeAsync(Guid id, UserChallenge userChallenge)
        {
            try
            {
                UserChallenge search = await _context.UserChallenges.FindAsync(id);
                if (search == null)
                {
                    return new globalResponds("0", "không tìm thấy ", null);
                }
                _context.UserChallenges.Entry(search).CurrentValues.SetValues(userChallenge);
                _context.UserChallenges.Update(search);
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
