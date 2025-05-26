using KafeAPI.Application.Dtos.CategoryDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
        Task<DetailCategoryDto> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CreateCategoryDto category);
        Task UpdateCategoryAsync(UpdateCategoryDto category);
        Task DeleteCategoryAsync(int id);
    }
}
