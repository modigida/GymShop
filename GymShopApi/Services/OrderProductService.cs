using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class OrderProductService(IUnitOfWork unitOfWork) : GenericService<OrderProduct>(unitOfWork)
{
    //TODO
    public override Task<OrderProduct> AddAsync(OrderProduct entity)
    {
        throw new NotImplementedException();
    }

    public override Task<OrderProduct> Update(int id, OrderProduct entity)
    {
        throw new NotImplementedException();
    }
}
