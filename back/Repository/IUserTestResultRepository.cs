using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IUserTestResultRepository
    {
        public Task<globalResponds> GetAll();
        public Task<globalResponds> Create(UserTestResult userTestResult, User user, Test test);
        public Task<globalResponds> Delete(UserTestResult userTestResult);
        public Task<globalResponds> Update(Guid id, UserTestResult userTestResult);

        public Task<globalResponds> GetById(Guid id);

        public Task<globalResponds> DeleteResultAndTest(UserTestResult userTestResult, Test test);

        public Task<globalResponds> DeleteResultAndUser(UserTestResult userTestResult, User test);
    }
}
