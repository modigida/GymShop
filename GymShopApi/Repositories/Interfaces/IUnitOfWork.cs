using GymShopApi.Entities;

namespace GymShopApi.Repositories.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> Categories { get; }
    IOrderRepository Orders { get; }
    IOrderProductRepository OrderProducts { get; }
    IGenericRepository<OrderStatus> OrderStatuses { get; }
    IProductRepository Products { get; }
    IGenericRepository<ProductStatus> ProductStatuses { get; }
    IGenericRepository<Role> Roles { get; }
    IUserRepository Users { get; }
    Task<int> CompleteAsync();
    IGenericRepository<T> GetRepository<T>() where T : class;
}