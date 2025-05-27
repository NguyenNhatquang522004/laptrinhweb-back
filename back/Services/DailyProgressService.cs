using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class DailyProgressService : IDailyProgressRepository

    {
        private readonly ApplicationDbContext _context;
        public DailyProgressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds> AddDailyProgressAsync(Guid userId, DateTime date, int progress)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> DeleteDailyProgressAsync(Guid userId, DateTime date)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> DeleteProgressAndUserAsync(User userId, DailyProgress dailyProgress)
        {
            try
            {
                userId.DailyProgresses.Remove(dailyProgress);
                _context.DailyProgresses.Remove(dailyProgress);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công ", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> GetAllProgressAsync(Guid userId)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> GetDailyProgressAsync(Guid userId, DateTime date)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> GetMonthlyProgressAsync(Guid userId, DateTime month)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> GetWeeklyProgressAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> GetYearlyProgressAsync(Guid userId, DateTime year)
        {
            try
            {
                List<DailyProgress> dailyProgressList = await _context.DailyProgresses
                    .Where(dp => dp.UserId == userId)
                    .ToListAsync();
                return new globalResponds("1", "thành công ", dailyProgressList);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }

        public async Task<globalResponds> UpdateDailyProgressAsync(Guid userId, DateTime date, int progress)
        {
            try
            {
                DailyProgress dailyProgress = await _context.DailyProgresses
                          .FirstOrDefaultAsync(dp => dp.UserId == userId && dp.Date.Date == date.Date);
                return new globalResponds("1", "thành công ", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }
        }
    }
}
