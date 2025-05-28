using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItemsAsync();
        Task<ResponseDto<DetailMenuItemDto>> GetMenuItemByIdAsync(int id);
        Task<ResponseDto<object>> AddMenuItemAsync(CreateMenuItemDto menuItem);
        Task<ResponseDto<object>> UpdateMenuItemAsync(UpdateMenuItemDto menuItem);
        Task<ResponseDto<object>> DeleteMenuItemAsync(int id);
    }
}
