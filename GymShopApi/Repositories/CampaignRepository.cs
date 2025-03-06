using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class CampaignRepository(AppDbContext context) : GenericRepository<Campaign>(context)
{
}
