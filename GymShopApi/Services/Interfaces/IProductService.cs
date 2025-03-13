using GymShopApi.DTOs;

namespace GymShopApi.Services.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDto?>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProductDto?>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<ProductDto?>> GetByStatusAsync(int statusId);
    Task<ProductDto> AddAsync(ProductDto productDto);
    Task<ProductDto> Update(ProductDto entity, params object[] keyValues);
    Task Delete(params object[] keyValues);
}
