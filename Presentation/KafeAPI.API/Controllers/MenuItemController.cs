using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var menuItems = await _menuItemServices.GetAllMenuItemsAsync();
            if (!menuItems.Success)
            {
                if (menuItems.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(menuItems);
                }
                return BadRequest();
            }
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemServices.GetMenuItemByIdAsync(id);
            if (!menuItem.Success)
            {
                if (menuItem.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(menuItem);
                }
                return BadRequest();
            }
            return Ok(menuItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(CreateMenuItemDto createMenuItemDto)
        {
            var result = await _menuItemServices.AddMenuItemAsync(createMenuItemDto);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest();
            }

            return Ok("Menü öğesi oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto updateMenuItemDto)
        {
            var result = await _menuItemServices.UpdateMenuItemAsync(updateMenuItemDto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.NotFound or ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _menuItemServices.DeleteMenuItemAsync(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
