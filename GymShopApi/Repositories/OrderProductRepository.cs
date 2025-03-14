using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymShopApi.Repositories;

public class OrderProductRepository(AppDbContext context)
    : GenericRepository<OrderProduct>(context), IOrderProductRepository
{
    public async Task AddRangeAsync(IEnumerable<OrderProduct> orderProducts)
    {
        if (orderProducts == null || !orderProducts.Any())
        {
            throw new ArgumentException("OrderProducts list cannot be null or empty.");
        }

        await _context.OrderProducts.AddRangeAsync(orderProducts);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderProducts
            .Where(op => op.OrderId == orderId)
            .Include(op => op.Product)
            .ToListAsync();
    }
    public async Task<bool> AnyAsync(int productId)
    {
        return await _context.OrderProducts.AnyAsync(op => op.ProductId == productId);
    }


    public async Task DeleteRangeAsync(IEnumerable<OrderProduct> orderProducts)
    {
        if (orderProducts == null || !orderProducts.Any())
        {
            throw new ArgumentException("OrderProducts list cannot be null or empty.");
        }

        _context.OrderProducts.RemoveRange(orderProducts);
        await _context.SaveChangesAsync();
    }
}
