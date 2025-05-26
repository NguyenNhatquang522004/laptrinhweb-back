using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class TestService : ITestRepository
    {
        private readonly ApplicationDbContext _context;
        public TestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds> CreateTestAsync(Test test)
        {
            try
            {
                _context.Tests.Add(test);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Tests retrieved successfully.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> DeleteTestAsync(Guid testId)
        {
            try
            {
                Test search = await _context.Tests.FindAsync(testId);
                _context.Tests.Remove(search);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Tests retrieved successfully.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetAllTestsAsync()
        {
            try
            {
                List<Test> tests = await _context.Tests.ToListAsync();
                return new globalResponds("1", "Tests retrieved successfully.", tests);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetPremiumTestsAsync()
        {
            try
            {
                List<Test> tests = await _context.Tests.Where(t => t.IsPremium).ToListAsync();
                if (tests == null || tests.Count == 0)
                {
                    return new globalResponds("0", "No premium tests found.", null);
                }
                return new globalResponds("1", "Tests retrieved successfully.", tests);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetTestByIdAsync(Guid testId)
        {
            try
            {
                Test search = await _context.Tests.FindAsync(testId);
                return new globalResponds("1", "Tests retrieved successfully.", search);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetTestsByLevelAsync(string cefrLevel)
        {
            try
            {
                List<Test> tests = await _context.Tests.Where(t => t.Level.ToString().Equals(cefrLevel, StringComparison.OrdinalIgnoreCase)).ToListAsync();
                if (tests == null || tests.Count == 0)
                {
                    return new globalResponds("0", "No tests found for the specified level.", null);
                }
                return new globalResponds("1", "Tests retrieved successfully.", tests);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetTestsByTypeAsync(string testType)
        {
            try
            {
                List<Test> tests = await _context.Tests.Where(t => t.TestType.ToString().Equals(testType, StringComparison.OrdinalIgnoreCase)).ToListAsync();
                if (tests == null || tests.Count == 0)
                {
                    return new globalResponds("0", "No tests found for the specified type.", null);
                }
                return new globalResponds("1", "Tests retrieved successfully.", tests);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> GetTestsByUserIdAsync(Guid userId)
        {
            try
            {
                List<Test> tests = await _context.Tests.Include(o => o.UserTestResults)
                                                        .Where(t => t.UserTestResults.Any(utr => utr.UserId == userId))
                                                        .ToListAsync();
                return new globalResponds("1", "Tests retrieved successfully.", tests);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }

        public async Task<globalResponds> UpdateTestAsync(Test test)
        {
            try
            {
                _context.Tests.Update(test);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Tests retrieved successfully.", null);
            }
            catch (Exception ex)
            {
                return new globalResponds("0", "An error occurred while retrieving tests." + ex.Message, null);
            }
        }


    }
}
