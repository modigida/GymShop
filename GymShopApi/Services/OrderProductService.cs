using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class OrderProductService(IUnitOfWork unitOfWork) : GenericService<OrderProduct>(unitOfWork)
{
}
