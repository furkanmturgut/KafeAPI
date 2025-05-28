using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryServices.GetAllCategoriesAsync();
            if (!categories.Success)
            {
                if (categories.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(categories);
                }

                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryServices.GetCategoryByIdAsync(id);
            if (!category.Success)
            {
                if (category.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(category);
                }
                return BadRequest();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryServices.AddCategoryAsync(createCategoryDto);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            return Ok("Kategori oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryServices.UpdateCategoryAsync(updateCategoryDto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.NotFound or ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            return Ok("Kategori güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryServices.DeleteCategoryAsync(id);
            if (!category.Success)
            {
                if (category.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(category);
                }
                return BadRequest();
            }
            return Ok("Kategori silindi");
        }
    }
}
