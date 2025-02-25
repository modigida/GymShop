using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IGenericRepository<Category> Categories { get; }
    public IGenericRepository<Order> Orders { get; }
    public IGenericRepository<OrderProduct> OrderProducts { get; }
    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<Role> Roles { get; }
    public IGenericRepository<OrderStatus> Statuses { get; }
    public IGenericRepository<User> Users { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Categories = new GenericRepository<Category>(_context);
        Orders = new GenericRepository<Order>(_context);
        OrderProducts = new GenericRepository<OrderProduct>(_context);
        Products = new GenericRepository<Product>(_context);
        Roles = new GenericRepository<Role>(_context);
        Statuses = new GenericRepository<OrderStatus>(_context);
        Users = new GenericRepository<User>(_context);
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

