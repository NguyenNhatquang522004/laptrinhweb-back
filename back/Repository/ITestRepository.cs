using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface ITestRepository
    {
        public Task<globalResponds> GetAllTestsAsync();
        public Task<globalResponds> GetTestByIdAsync(Guid testId);
        public Task<globalResponds> CreateTestAsync(Test test);
        public Task<globalResponds> UpdateTestAsync(Test test);
        public Task<globalResponds> GetTestsByUserIdAsync(Guid userId);
        public Task<globalResponds> GetPremiumTestsAsync();

        public Task<globalResponds> GetTestsByTypeAsync(string testType);
        public Task<globalResponds> GetTestsByLevelAsync(string cefrLevel);
    }
}
