using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface ILessonRepository
    {
        public Task<globalResponds?> GetLessonByIdAsync(Guid lessonId);
        public Task<globalResponds> GetAllLessonsAsync();
        public Task<globalResponds> CreateLessonAsync(Lesson lesson);
        public Task<globalResponds> UpdateLessonAsync(Lesson lesson);
        public Task<globalResponds> DeleteLessonAsync(Guid lessonId);
        public Task<globalResponds> GetLessonsByCategoryAsync(Guid categoryId);
        //public Task<globalResponds> GetLessonsByUserAsync(Guid userId);

        public Task<globalResponds> GetLessonsByLevelAsync(string level);
        public Task<globalResponds> CreateLessonAndCategory(Lesson lessonId, Category categoryI);

        public Task<globalResponds> CreateLessonAndExercise(Lesson lessonid, Exercise exerciseid);

        //public Task<globalResponds> CreateLessonAndUserProgress(Lesson lessonId, Guid userId);


        public Task<globalResponds> DeleteLessonAndCategory(Lesson lesson);


        public Task<globalResponds> DeleteLessonAndExercise(Lesson lesson, Exercise exerciseId);




    }
}
