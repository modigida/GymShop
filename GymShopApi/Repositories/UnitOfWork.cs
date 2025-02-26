using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IGenericRepository<Category> Categories { get; }
    public IGenericRepository<Order> Orders { get; }
    public IGenericRepository<OrderProduct> OrderProducts { get; }
    public IGenericRepository<OrderStatus> OrderStatuses { get; }
    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<ProductStatus> ProductStatuses { get; }
    public IGenericRepository<Role> Roles { get; }
    public IGenericRepository<User> Users { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Categories = new GenericRepository<Category>(_context);
        Orders = new GenericRepository<Order>(_context);
        OrderProducts = new GenericRepository<OrderProduct>(_context);
        OrderStatuses = new GenericRepository<OrderStatus>(_context);
        Products = new GenericRepository<Product>(_context);
        Roles = new GenericRepository<Role>(_context);
        ProductStatuses = new GenericRepository<ProductStatus>(_context);
        Users = new GenericRepository<User>(_context);
    }

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories[typeof(T)] = new GenericRepository<T>(_context);
        }
        return (IGenericRepository<T>)_repositories[typeof(T)];
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}

