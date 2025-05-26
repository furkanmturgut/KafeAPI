using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<List<ResultMenuItemDto>> GetAllMenuItemsAsync();
        Task<DetailMenuItemDto> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(CreateMenuItemDto menuItem);
        Task UpdateMenuItemAsync(UpdateMenuItemDto menuItem);
        Task DeleteMenuItemAsync(int id);
    }
}
