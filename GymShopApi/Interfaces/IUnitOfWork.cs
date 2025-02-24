using GymShopApi.Models;

namespace GymShopApi.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Order> Orders { get; }
    IGenericRepository<OrderProduct> OrderProducts { get; }
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<Status> Statuses { get; }
    IGenericRepository<User> Users { get; }
    Task<int> CompleteAsync();
}