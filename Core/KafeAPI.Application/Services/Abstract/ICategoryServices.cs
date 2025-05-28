using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategoriesAsync();
        Task<ResponseDto<DetailCategoryDto>> GetCategoryByIdAsync(int id);
        Task<ResponseDto<object>> AddCategoryAsync(CreateCategoryDto category);
        Task<ResponseDto<object>> UpdateCategoryAsync(UpdateCategoryDto category);
        Task<ResponseDto<object>> DeleteCategoryAsync(int id);
    }
}
