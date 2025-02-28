using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class ProductStatusService(IUnitOfWork unitOfWork) : GenericService<ProductStatus>(unitOfWork)
{
}
