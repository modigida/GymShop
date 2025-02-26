using GymShopApi.Database;
using GymShopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymShopApi.Repositories;
public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChangesAsync();
    }
    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChangesAsync();
    }
}
