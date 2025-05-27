using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class ChallengeService : IChallengeRepository
    {
        private readonly ApplicationDbContext _context;
        public ChallengeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds> CreateChallengeAsync(Challenge challenge, User user)
        {
            try
            {

                return new globalResponds("1", "Challenges retrieved successfully", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving challenges: " + e.Message, null);
            }
        }

        public Task<globalResponds> DeleteChallengeAsync(Guid challengeId)
        {
            throw new NotImplementedException();
        }

        public async Task<globalResponds> GetAllChallengesAsync()
        {
            try
            {
                var challenges = await _context.Challenges.ToListAsync();
                return new globalResponds("1", "Challenges retrieved successfully", challenges);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving challenges: " + e.Message, null);
            }
        }



        public async Task<globalResponds> GetChallengeByIdAsync(Guid challengeId)
        {
            try
            {
                var challenges = await _context.Challenges.ToListAsync();
                return new globalResponds("1", "Challenges retrieved successfully", challenges);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving challenges: " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetChallengesByCategoryAsync(Guid userId, string category)
        {
            try
            {
                var challenges = await _context.Challenges.ToListAsync();
                return new globalResponds("1", "Challenges retrieved successfully", challenges);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving challenges: " + e.Message, null);
            }
        }

        public async Task<globalResponds> GetChallengesByDifficultyAsync(Guid userId, string difficulty)
        {
            try
            {
                var challenges = await _context.Challenges.ToListAsync();
                return new globalResponds("1", "Challenges retrieved successfully", challenges);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving challenges: " + e.Message, null);
            }
        }

        public async Task<globalResponds> UpdateChallengeAsync(Challenge challenge)
        {
            try
            {
                var challenges = await _context.Challenges.ToListAsync();
                return new globalResponds("1", "Challenges retrieved successfully", challenges);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving challenges: " + e.Message, null);
            }
        }
    }
}
