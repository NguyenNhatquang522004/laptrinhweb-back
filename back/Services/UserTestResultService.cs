using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class UserTestResultService : IUserTestResultRepository
    {
        private readonly ApplicationDbContext _context;
        public UserTestResultService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<globalResponds> GetById(Guid id)
        {
            try
            {
                UserTestResult list = await _context.UserTestResults.FindAsync(id);
                return new globalResponds("1", "thành công ", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }
        public async Task<globalResponds> Create(UserTestResult userTestResult, User user, Test test)
        {
            try
            {
                userTestResult.UserId = user.UserId;
                userTestResult.TestId = test.TestId;
                user.UserTestResults.Add(userTestResult);
                test.UserTestResults.Add(userTestResult);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công ", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> Delete(UserTestResult userTestResult)
        {
            try
            {
                userTestResult.User.UserTestResults.Remove(userTestResult);
                userTestResult.Test.UserTestResults.Remove(userTestResult);
                _context.UserTestResults.Remove(userTestResult);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công ", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteResultAndTest(UserTestResult userTestResult, Test test)
        {
            try
            {

                List<UserTestResult> list = await _context.UserTestResults.ToListAsync();
                return new globalResponds("1", "thành công ", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> DeleteResultAndUser(UserTestResult userTestResult, User test)
        {
            try
            {
                List<UserTestResult> list = await _context.UserTestResults.ToListAsync();
                return new globalResponds("1", "thành công ", null);
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
                List<UserTestResult> list = await _context.UserTestResults.ToListAsync();
                return new globalResponds("1", "thành công ", list);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }

        public async Task<globalResponds> Update(Guid id, UserTestResult userTestResult)
        {
            try
            {
                UserTestResult search = await _context.UserTestResults.FindAsync(id);
                if (search == null)
                {
                    return new globalResponds("0", "không tìm thấy ", null);

                }
                _context.UserTestResults.Entry(search).CurrentValues.SetValues(userTestResult);
                _context.UserTestResults.Update(search);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công ", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công " + e.Message, null);
            }
        }
    }
}
