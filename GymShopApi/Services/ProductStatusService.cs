using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class ProductStatusService(IUnitOfWork unitOfWork) : GenericService<ProductStatus>(unitOfWork)
{
    public async Task<bool> NameExistsAsync(string name)
    {
        var productStatuses = await unitOfWork.ProductStatuses.GetAllAsync();
        return productStatuses.Any(p => p.Name == name);
    }
    public override async Task<ProductStatus> AddAsync(ProductStatus entity)
    {
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Product status name is required.");
        }
        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Product status with the same name already exists.");
        }

        await unitOfWork.ProductStatuses.AddAsync(entity);
        await unitOfWork.CompleteAsync();

        return entity;
    }

    public override async Task<ProductStatus> Update(ProductStatus entity, params object[] keyValues)
    {
        var productStatus = await GetByIdAsync(keyValues);
        if (productStatus == null)
        {
            throw new ArgumentException("Category not found.");
        }
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Category name is required.");
        }

        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Category with the same name already exists.");
        }

        productStatus.Name = entity.Name;

        await unitOfWork.ProductStatuses.Update(productStatus);
        await unitOfWork.CompleteAsync();

        return productStatus;
    }
}
