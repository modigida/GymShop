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
            .ToListAsync();

        foreach (var order in orders)
        {
            var orderProducts = await _context.OrderProducts
                .Include(op => op.Product)
                .Where(op => op.OrderId == order.Id)
                .ToListAsync();
        }

        return orders;
    }
    public override async Task<Order?> GetByIdAsync(params object[] keyValues)
    {
        var order = await _context.Orders
            .Include(o => o.User)
                .ThenInclude(u => u.Role)
            .Include(o => o.OrderStatus)
            .FirstOrDefaultAsync(o => o.Id == (int)keyValues[0]);
        if (order != null)
        {
            order.OrderProducts = await _context.OrderProducts
                .Include(op => op.Product)
                .Where(op => op.OrderId == order.Id)
                .ToListAsync();
        }
        return order;
    }

    public async Task<IEnumerable<Order?>> GetByEmailAsync(string email)
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .ThenInclude(u => u.Role)
            .Include(o => o.OrderStatus)
            .Where(o => o.User.Email == email)
            .ToListAsync();

        foreach (var order in orders)
        {
            var orderProducts = await _context.OrderProducts
                .Include(op => op.Product)
                .Where(op => op.OrderId == order.Id)
                .ToListAsync();
        }

        return orders;
    }
}

