using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymShopApi.Repositories;
public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public override async Task<IEnumerable<Product?>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductStatus)
            .ToListAsync();
    }
    public override async Task<Product?> GetByIdAsync(params object[] keyValues)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductStatus)
            .FirstOrDefaultAsync(p => p.Id == (int)keyValues[0]);
    }

    public async Task<IEnumerable<Product?>> GetByCategory(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductStatus)
            .Where(p => p.Category.Id == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetByStatus(int statusId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductStatus)
            .Where(p => p.ProductStatus.Id == statusId)
            .ToListAsync();
    }
}
