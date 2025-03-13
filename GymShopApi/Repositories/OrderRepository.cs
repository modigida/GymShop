using System.Runtime.CompilerServices;
using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymShopApi.Repositories;
public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public override async Task<IEnumerable<Order?>> GetAllAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.User)
                .ThenInclude(u => u.Role)
            .Include(o => o.OrderStatus)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category)
            .ToListAsync();

        return orders;
    }
    public override async Task<Order?> GetByIdAsync(params object[] keyValues)
    {
        var order = await _context.Orders
            .Include(o => o.User)
                .ThenInclude(u => u.Role)
            .Include(o => o.OrderStatus)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(o => o.Id == (int)keyValues[0]);


        return order;
    }

    public async Task<IEnumerable<Order?>> GetByEmailAsync(string email)
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .ThenInclude(u => u.Role)
            .Include(o => o.OrderStatus)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category)
            .Where(o => o.User.Email == email)
            .ToListAsync();

        return orders;
    }
}

