using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;
using static backapi.Enums.enums;

namespace backapi.Services
{
    public class LessonService : ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<globalResponds?> GetLessonByIdAsync(Guid lessonId)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(lessonId);
                if (lesson == null)
                {
                    return new globalResponds("0", "Lesson not found", null);
                }
                return new globalResponds("1", "Lesson found", lesson);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving lesson: " + e.Message, null);
            }

        }
        public async Task<globalResponds> GetAllLessonsAsync()
        {
            try
            {
                var lessons = await _context.Lessons.ToListAsync();
                if (lessons == null || !lessons.Any())
                {
                    return new globalResponds("0", "No lessons found", null);
                }
                return new globalResponds("1", "Lessons retrieved successfully", lessons);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving lessons: " + e.Message, null);
            }
        }

        public async Task<globalResponds> CreateLessonAsync(Lesson lesson, Category category)
        {
            try
            {
                if (lesson == null)
                {
                    return new globalResponds("0", "Lesson cannot be null", null);
                }
                if (category == null)
                {
                    return new globalResponds("0", "Category cannot be null", null);
                }
                lesson.Category = category;
                lesson.CategoryId = category.CategoryId;
                category.Lessons.Add(lesson);
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Lesson created successfully", lesson);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error creating lesson: " + e.Message, null);
            }
        }

        public async Task<globalResponds> UpdateLessonAsync(Lesson lesson)
        {
            try
            {
                if (lesson == null)
                {
                    return new globalResponds("0", "Lesson cannot be null", null);
                }
                _context.Entry(lesson).CurrentValues.SetValues(lesson);
                _context.Lessons.Update(lesson);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Lesson updated successfully", lesson);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error updating lesson: " + e.Message, null);
            }
        }
        public async Task<globalResponds> DeleteLessonAsync(Guid lessonId)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(lessonId);
                if (lesson == null)
                {
                    return new globalResponds("0", "Lesson not found", null);
                }
                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Lesson deleted successfully", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error deleting lesson: " + e.Message, null);
            }
        }
        public async Task<globalResponds> GetLessonsByCategoryAsync(Guid categoryId)
        {
            try
            {
                var lessons = await _context.Lessons.Where(l => l.CategoryId == categoryId).ToListAsync();
                if (lessons == null || !lessons.Any())
                {
                    return new globalResponds("0", "No lessons found for this category", null);
                }
                return new globalResponds("1", "Lessons retrieved successfully", lessons);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving lessons by category: " + e.Message, null);
            }
        }



        public async Task<globalResponds> GetLessonsByLevelAsync(string level)
        {
            try
            {
                if (string.IsNullOrEmpty(level) || !Enum.TryParse<CefrLevel>(level, out _))
                {
                    return new globalResponds("0", "Invalid level provided", null);
                }
                var cefrLevel = Enum.Parse<CefrLevel>(level, true);
                var lessons = await _context.Lessons.Where(l => l.Level == cefrLevel).ToListAsync();
                if (lessons == null || !lessons.Any())
                {
                    return new globalResponds("0", "No lessons found for this level", null);
                }
                return new globalResponds("1", "Lessons retrieved successfully", lessons);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving lessons by level: " + e.Message, null);
            }
        }
        public async Task<globalResponds> CreateLessonAndCategory(Lesson lessonId, Category categoryId)
        {
            try
            {
                lessonId.Category = categoryId;
                lessonId.CategoryId = categoryId.CategoryId; // Assuming Category has an Id property    
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Lesson and category linked successfully", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error linking lesson and category: " + e.Message, null);
            }
        }

        public async Task<globalResponds> CreateLessonAndExercise(Lesson lessonId, Exercise exerciseId)
        {
            try
            {
                exerciseId.Lesson = lessonId;
                exerciseId.LessonId = lessonId.LessonId; // Assuming Lesson has an Id property
                lessonId.Exercises.Add(exerciseId);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Lesson and exercise linked successfully", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error linking lesson and exercise: " + e.Message, null);
            }
        }


        public async Task<globalResponds> DeleteLessonAndCategory(Lesson lesson)
        {
            try
            {
                if (lesson.Category != null)
                {
                    _context.Categories.Remove(lesson.Category);
                }
                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "Lesson and category deleted successfully", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error deleting lesson and category: " + e.Message, null);
            }
        }
        public async Task<globalResponds> DeleteLessonAndExercise(Lesson lesson, Exercise exerciseId)
        {
            try
            {
                lesson.Exercises.Remove(exerciseId);
                await _context.SaveChangesAsync();

                return new globalResponds("1", "Lesson and exercise deleted successfully", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error deleting lesson and Exercise: " + e.Message, null);
            }

        }
    }
}