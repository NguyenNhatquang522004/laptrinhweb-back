using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface IDailyProgressRepository
    {
        public Task<globalResponds> GetDailyProgressAsync(Guid userId, DateTime date);
        public Task<globalResponds> AddDailyProgressAsync(Guid userId, DateTime date, int progress);
        public Task<globalResponds> UpdateDailyProgressAsync(Guid userId, DateTime date, int progress);
        public Task<globalResponds> DeleteDailyProgressAsync(Guid userId, DateTime date);
        public Task<globalResponds> GetWeeklyProgressAsync(Guid userId, DateTime startDate, DateTime endDate);
        public Task<globalResponds> GetMonthlyProgressAsync(Guid userId, DateTime month);
        public Task<globalResponds> GetYearlyProgressAsync(Guid userId, DateTime year);

        public Task<globalResponds> GetAllProgressAsync(Guid userId);
        public Task<globalResponds> DeleteProgressAndUserAsync(User userId, DailyProgress dailyProgress);



    }
}
