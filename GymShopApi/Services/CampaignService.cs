using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services
{
    public class CampaignService(IUnitOfWork unitOfWork) : GenericService<Campaign>(unitOfWork)
    {
        public override async Task<Campaign> AddAsync(Campaign entity)
        {
            if (entity.StartDate == DateTime.MinValue || entity.Duration == TimeSpan.Zero)
            {
                throw new ArgumentException("Invalid input.");
            }

            await _unitOfWork.Campaigns.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }

        public override async Task<Campaign> Update(Campaign entity, params object[] keyValues)
        {
            var campaign = await GetByIdAsync(keyValues);
            if (campaign == null)
            {
                throw new ArgumentException("Campaign not found.");
            }
            if (entity == null)
            {
                throw new ArgumentException("Invalid input.");
            }

            if (entity.StartDate != DateTime.MinValue && entity.StartDate != campaign.StartDate)
            {
                campaign.StartDate = entity.StartDate;
            }
            if (entity.Duration != TimeSpan.Zero && entity.Duration != campaign.Duration)
            {
                campaign.Duration = entity.Duration;
            }

            await _unitOfWork.Campaigns.Update(campaign);
            await _unitOfWork.CompleteAsync();
            return entity;

        }
    }
}
