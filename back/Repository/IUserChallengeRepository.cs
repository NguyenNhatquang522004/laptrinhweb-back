using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IUserChallengeRepository
    {
        public Task<globalResponds> CreateUserChallengeAsync(UserChallenge userChallenge, User user, Challenge challenge);
        public Task<globalResponds> GetUserChallengesByUserIdAsync(Guid userId);
        public Task<globalResponds> GetUserChallengeByIdAsync(Guid userChallengeId);
        public Task<globalResponds> UpdateUserChallengeAsync(Guid id, UserChallenge userChallenge);
        public Task<globalResponds> DeleteUserChallengeAsync(UserChallenge userChallengeId);
        public Task<globalResponds> GetUserChallengesByChallengeIdAsync(Guid challengeId);
        public Task<globalResponds> GetAllUserChallenges();
    }
}
