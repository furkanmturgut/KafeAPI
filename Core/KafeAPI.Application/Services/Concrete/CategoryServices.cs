using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>> { Success = true, Data = result };
            }
            catch (Exception e)
            {
                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = false,
                    Message = e.Message,
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailCategoryDto>> GetCategoryByIdAsync(int id)
        {

            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return new ResponseDto<DetailCategoryDto>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                var result = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto> { Success = true, Data = result };
            }
            catch (Exception e)
            {
                return new ResponseDto<DetailCategoryDto> { Success = false, Message = e.Message, ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> AddCategoryAsync(CreateCategoryDto dto)
        {
            try
            {
                var validationResult = await _createCategoryValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError,

                    };
                }
                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori eklendi"
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

        public async Task<ResponseDto<object>> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            try
            {
                var validationResult = await _updateCategoryValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError
                    };
                }

                var categoryDb = await _categoryRepository.GetByIdAsync(dto.Id);
                if (categoryDb == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }

                var category = _mapper.Map(dto, categoryDb);
                await _categoryRepository.UpdateAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori güncellendi"
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

        public async Task<ResponseDto<object>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                await _categoryRepository.DeleteAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori silindi"
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
