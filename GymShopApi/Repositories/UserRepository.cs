using GymShopApi.Database;
using GymShopApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymShopApi.Repositories;
public class UserRepository(AppDbContext context) : GenericRepository<User>(context)
{
}

