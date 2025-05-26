using backapi.Configuration;
using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.EntityFrameworkCore;

namespace backapi.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext _context)
        {
            _context = _context;
        }
        public async Task<globalResponds?> GetCategoryByIdAsync(Guid categoryId)
        {
            try
            {
                Category searchCate = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
                if (searchCate == null)
                {
                    return new globalResponds("0", "Category not found", null);
                }

                return new globalResponds("1", "Category found", searchCate);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving category: " + e.Message, null);
            }

        }
        public async Task<globalResponds> GetAllCategoriesAsync()
        {
            try
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                if (categories == null || !categories.Any())
                {
                    return new globalResponds("0", "No categories found", null);
                }
                return new globalResponds("1", "Categories retrieved successfully", categories);

            }
            catch (Exception e)
            {
                return new globalResponds("0", "Error retrieving categories: " + e.Message, null);
            }

        }
        public async Task<globalResponds> CreateCategoryAsync(Category category)
        {
            try
            {
                if (category == null)
                {
                    return new globalResponds("0", "Category cannot be null", null);
                }
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công ", null);

            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }


        }
        public async Task<globalResponds> UpdateCategoryAsync(Category category)
        {
            try
            {
                Category searchCate = await _context.Categories.FirstOrDefaultAsync(o => o.CategoryId == category.CategoryId);
                if (searchCate == null)
                {
                    return new globalResponds("0", "không tìm thấy cate update ", null);
                }
                _context.Entry(searchCate).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }

        }
        public async Task<globalResponds> DeleteCategoryAsync(Guid categoryId)
        {
            try
            {
                Category searchCate = await _context.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
                if (searchCate == null)
                {
                    return new globalResponds("0", "không tìm thấy cate ", null);
                }
                _context.Categories.Remove(searchCate);
                await _context.SaveChangesAsync();
                return new globalResponds("1", "thành công", null);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);
            }

        }
        public async Task<globalResponds> GetActiveCategoriesAsync()
        {
            try
            {
                List<Category> listcate = await _context.Categories.Where(o => o.IsActive == true).ToListAsync();
                return new globalResponds("1", "thành công", listcate);
            }
            catch (Exception e)
            {
                return new globalResponds("0", "không thành công", null);

            }
        }


    }
}
