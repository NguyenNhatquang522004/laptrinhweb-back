using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class TestQuestionService : ITestQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public TestQuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds> CreateTestQuestionAsync(TestQuestion testQuestion, Test test)
        {
            try
            {
                testQuestion.TestId = test.TestId;
                testQuestion.Test = test;
                test.TestQuestions.Add(testQuestion);
                _context.TestQuestions.Add(testQuestion);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Test question created successfully.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while creating the test question." + ex.Message, null);
            }
        }

        public async Task<globalResponds> DeleteTestQuestionAndTestAsync(TestQuestion questionId, Test testId)
        {
            try
            {
                questionId.QuestionId = Guid.Empty;
                questionId.TestId = Guid.Empty;
                testId.TestQuestions.Remove(questionId);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "không thành công" + ex.Message, null);
            }
        }
        public async Task<globalResponds> GetAllTestQuestionsAsync(Guid testId)
        {
            try
            {
                List<TestQuestion> list = await _context.TestQuestions
                    .Where(q => q.TestId == testId)
                    .ToListAsync();
                return new globalResponds("1", "Test questions retrieved successfully.", list);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving test questions." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetTestQuestionByIdAsync(Guid questionId)
        {
            try
            {
                TestQuestion list = await _context.TestQuestions.FindAsync(questionId);
                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }
        public async Task<globalResponds> GetTestQuestionsByUserIdAsync(Guid userId, Guid testId)
        {
            try
            {
                List<TestQuestion> list = await _context.TestQuestions
                            .Include(tq => tq.Test)
                                .ThenInclude(t => t.UserTestResults.Where(utr => utr.UserId == userId))
                            .Where(tq => tq.TestId == testId)
                            .ToListAsync();

                return new globalResponds("1", "thành công", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> UpdateTestQuestionAsync(TestQuestion testQuestion)
        {
            try
            {
                _context.TestQuestions.Update(testQuestion);
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
