using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services
{
    public class CampaignProductService(IUnitOfWork unitOfWork) : GenericService<CampaignProduct>(unitOfWork)
    {
        public override Task<CampaignProduct> AddAsync(CampaignProduct entity)
        {
            throw new NotImplementedException();
        }

        public override Task<CampaignProduct> Update(CampaignProduct entity, params object[] keyValues)
        {
            throw new NotImplementedException();
        }
    }
}
