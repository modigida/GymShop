using GymShopApi.Entities;

namespace GymShopApi.Repositories.Interfaces;
public interface IOrderProductRepository : IGenericRepository<OrderProduct>
{
    Task AddRangeAsync(IEnumerable<OrderProduct> orderProducts);
    Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(int orderId);
    Task DeleteRangeAsync(IEnumerable<OrderProduct> orderProducts);
}
