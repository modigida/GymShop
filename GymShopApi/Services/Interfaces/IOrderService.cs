using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services.Interfaces;
public interface IOrderService
{
    Task<IEnumerable<OrderResponseDto?>> GetAllAsync();
    Task<OrderResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<OrderResponseDto?>> GetByEmailAsync(string email);
    Task<OrderResponseDto> AddAsync(OrderCreateDto entity);
    Task<OrderResponseDto> Update(OrderCreateDto entity, params object[] keyValues);
    Task Delete(params object[] keyValues);
}
