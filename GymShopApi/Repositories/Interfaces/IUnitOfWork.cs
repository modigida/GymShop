using GymShopApi.Models;

namespace GymShopApi.Repositories.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Order> Orders { get; }
    IGenericRepository<OrderProduct> OrderProducts { get; }
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<OrderStatus> Statuses { get; }
    IGenericRepository<User> Users { get; }
    Task<int> CompleteAsync();
}