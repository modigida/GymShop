using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class RoleRepository(AppDbContext context) : GenericRepository<Role>(context)
{
}
