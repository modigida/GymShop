using GymShopApi.Database;
using GymShopApi.Entities;

namespace GymShopApi.Repositories;
public class CampaignProductRepository(AppDbContext context) : GenericRepository<CampaignProduct>(context)
{
}
