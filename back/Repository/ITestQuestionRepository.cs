using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface ITestQuestionRepository
    {
        public Task<globalResponds> GetAllTestQuestionsAsync(Guid testId);
        public Task<globalResponds> GetTestQuestionByIdAsync(Guid questionId);
        public Task<globalResponds> CreateTestQuestionAsync(TestQuestion testQuestion, Test test);
        public Task<globalResponds> UpdateTestQuestionAsync(TestQuestion testQuestion);
        public Task<globalResponds> GetTestQuestionsByUserIdAsync(Guid userId, Guid testId);


        public Task<globalResponds> DeleteTestQuestionAndTestAsync(TestQuestion questionId, Test testId);

    }
}
