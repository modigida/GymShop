using GymShopApi.Database;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymShopApi.Repositories;
public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public override async Task<IEnumerable<User?>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Role)
            .ToListAsync();
    }
    public override async Task<User?> GetByIdAsync(params object[] keyValues)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id.ToString() == keyValues[0].ToString());
    }
}

