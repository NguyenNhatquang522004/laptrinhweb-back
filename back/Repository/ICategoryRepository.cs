using backapi.Helpers;
using backapi.Model;

namespace backapi.Repository
{
    public interface ICategoryRepository
    {
        Task<globalResponds?> GetCategoryByIdAsync(Guid categoryId);
        Task<globalResponds> GetAllCategoriesAsync();
        Task<globalResponds> CreateCategoryAsync(Category category);
        Task<globalResponds> UpdateCategoryAsync(Category category);
        Task<globalResponds> DeleteCategoryAsync(Guid categoryId);
        Task<globalResponds> GetActiveCategoriesAsync();
    }
}
