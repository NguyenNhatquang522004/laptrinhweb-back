using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IChallengeRepository
    {
        public Task<globalResponds> GetAllChallengesAsync();
        public Task<globalResponds> GetChallengeByIdAsync(Guid challengeId);
        public Task<globalResponds> CreateChallengeAsync(Challenge challenge, User user);
        public Task<globalResponds> UpdateChallengeAsync(Challenge challenge);
        public Task<globalResponds> DeleteChallengeAsync(Guid challengeId);
        public Task<globalResponds> GetChallengesByCategoryAsync(Guid userId, string category);

        public Task<globalResponds> GetChallengesByDifficultyAsync(Guid userId, string difficulty);
    }
}
