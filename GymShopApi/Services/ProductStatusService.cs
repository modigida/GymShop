using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class ProductStatusService(IUnitOfWork unitOfWork) : GenericService<ProductStatus>(unitOfWork)
{
    //TODO
    public override Task<ProductStatus> AddAsync(ProductStatus entity)
    {
        throw new NotImplementedException();
    }

    public override Task<ProductStatus> Update(int id, ProductStatus entity)
    {
        throw new NotImplementedException();
    }
}
