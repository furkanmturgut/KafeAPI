using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator, IGenericRepository<Category> categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _createMenuItemValidator = createMenuItemValidator;
            _updateMenuItemValidator = updateMenuItemValidator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItemsAsync()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        Success = false,
                        Message = "Menü öğesi bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>> { Success = true, Data = result };
            }
            catch (Exception e)
            {
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = false,
                    Message = "Hata meydana geldi",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetMenuItemByIdAsync(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                var category = await _categoryRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<DetailMenuItemDto>
                    {
                        Success = false,
                        Message = "Menü öğesi bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                var result = _mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto> { Success = true, Data = result };
            }
            catch (Exception e)
            {
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = false,
                    Message = "Hata meydana geldi",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> AddMenuItemAsync(CreateMenuItemDto dto)
        {
            try
            {
                var validationResult = await _createMenuItemValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError,
                    };
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _menuItemRepository.AddAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Menü öğesi başarıyla eklendi",
                    Data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Hata meydana geldi",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateMenuItemAsync(UpdateMenuItemDto dto)
        {
            try
            {
                var validationResult = await _updateMenuItemValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError,
                    };
                }
                var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Menü öğesi bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                var newMenuItem = _mapper.Map(dto, menuItem);
                await _menuItemRepository.UpdateAsync(newMenuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Menü öğesi başarıyla güncellendi",
                    Data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Hata meydana geldi",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteMenuItemAsync(int id)
        {
            try
            {
                var menuItem = _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Menü öğesi bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                await _menuItemRepository.DeleteAsync(menuItem.Result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Menü öğesi başarıyla silindi",
                    Data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Hata meydana geldi",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }
    }
}
