using backapi.Helpers;
using backapi.Model;
using backapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Xóa category theo ID
        /// </summary>
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(Guid categoryId)
        {
            try
            {
                globalResponds response = await _categoryService.DeleteCategoryAsync(categoryId);
                if (response.Code == "0")
                {
                    return NotFound(response.Message);
                }
                return Ok(new globalResponds("200", "thành công", null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}", categoryId);
                return StatusCode(500, new globalResponds("500", "Internal server error", null));
            }
        }

        /// <summary>
        /// Tạo category mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new globalResponds("400", "Invalid category data", null));
            }
            try
            {
                globalResponds response = await _categoryService.CreateCategoryAsync(category);
                if (response.Code == "0")
                {
                    return BadRequest(response.Message);
                }
                return Ok(new globalResponds("1", "thành công", null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return StatusCode(500, new globalResponds("500", "Internal server error", null));
            }
        }

        /// <summary>
        /// Lấy category theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                globalResponds? response = await _categoryService.GetCategoryByIdAsync(id);
                if (response == null || response.Code == "0")
                {
                    return NotFound(response?.Message ?? "Category not found");
                }
                return Ok(new globalResponds("1", "thành công", response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category with ID {CategoryId}", id);
                return StatusCode(500, new globalResponds("500", "Internal server error", null));
            }
        }

        /// <summary>
        /// Lấy tất cả categories
        /// </summary>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                globalResponds response = await _categoryService.GetAllCategoriesAsync();
                if (response.Code == "0")
                {
                    return NotFound(response.Message);
                }
                return Ok(new globalResponds("1", "thành công", response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all categories");
                return StatusCode(500, new globalResponds("500", "Internal server error", null));
            }
        }

        /// <summary>
        /// Lấy tất cả categories đang hoạt động
        /// </summary>
        [HttpGet] // ✅ THÊM DÒNG NÀY - đây là lỗi chính
        [Route("active")] // ✅ THÊM ROUTE ĐỂ PHÂN BIỆT VỚI GetAllCategoriesAsync
        public async Task<IActionResult> GetActiveCategoriesAsync()
        {
            try
            {
                globalResponds response = await _categoryService.GetActiveCategoriesAsync();
                if (response.Code == "0")
                {
                    return NotFound(response.Message);
                }
                return Ok(new globalResponds("1", "thành công", response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active categories");
                return StatusCode(500, new globalResponds("500", "Internal server error", null));
            }
        }

        /// <summary>
        /// Cập nhật category
        /// </summary>
        [HttpPut] // ✅ THAY ĐỔI TỪ [HttpPost] THÀNH [HttpPut] CHO ĐÚNG RESTful
        [Route("update")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new globalResponds("400", "Invalid category data", null));
            }
            try
            {
                globalResponds response = await _categoryService.UpdateCategoryAsync(category);
                if (response.Code == "0")
                {
                    return BadRequest(response.Message);
                }
                return Ok(new globalResponds("1", "thành công", null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                return StatusCode(500, new globalResponds("500", "Internal server error", null));
            }
        }
    }
}