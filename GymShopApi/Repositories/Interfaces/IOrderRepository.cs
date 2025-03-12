using GymShopApi.Entities;

namespace GymShopApi.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order?>> GetByEmailAsync(string email);
    }
}
