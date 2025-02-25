using GymShopApi.Entities;

namespace GymShopApi.Repositories.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Order> Orders { get; }
    IGenericRepository<OrderProduct> OrderProducts { get; }
    IGenericRepository<OrderStatus> OrderStatuses { get; }
    IGenericRepository<Product> Products { get; }
    IGenericRepository<ProductStatus> ProductStatuses { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<User> Users { get; }
    Task<int> CompleteAsync();
}