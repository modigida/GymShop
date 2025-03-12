using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services.Interfaces;
public interface IOrderService
{
    Task<IEnumerable<OrderDto?>> GetAllAsync();
    Task<OrderDto?> GetByIdAsync(int id);
    Task<IEnumerable<OrderDto?>> GetByEmailAsync(string email);
    Task<OrderDto> AddAsync(OrderDto orderDto);
    Task<OrderDto> Update(OrderDto entity, params object[] keyValues);
    Task Delete(params object[] keyValues);
}
