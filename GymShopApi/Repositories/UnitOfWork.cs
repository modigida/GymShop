using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IGenericRepository<Category> Categories { get; }
    public IOrderRepository Orders { get; }
    public IOrderProductRepository OrderProducts { get; }
    public IGenericRepository<OrderStatus> OrderStatuses { get; }
    public IProductRepository Products { get; }
    public IGenericRepository<ProductStatus> ProductStatuses { get; }
    public IGenericRepository<Role> Roles { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Categories = new GenericRepository<Category>(_context);
        Orders = new OrderRepository(context);
        OrderProducts = new OrderProductRepository(_context);
        OrderStatuses = new GenericRepository<OrderStatus>(_context);
        Products = new ProductRepository(context);
        Roles = new GenericRepository<Role>(_context);
        ProductStatuses = new GenericRepository<ProductStatus>(_context);
        Users = new UserRepository(context);
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

