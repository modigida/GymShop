using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class ProductService(IUnitOfWork unitOfWork) : GenericService<Product>(unitOfWork)
{
    public override async Task<Product> AddAsync(Product entity)
    {
        if (string.IsNullOrEmpty(entity.Name) || entity.Balance <= 0.0 || entity.Price <= 0)
        {
            throw new ArgumentException("Invalid input.");
        }
        await unitOfWork.Products.AddAsync(entity);
        await unitOfWork.CompleteAsync();

        entity.Category = await unitOfWork.Categories.GetByIdAsync(entity.CategoryId);
        return entity;
    }
    public override async Task<Product> Update(Product entity, params object[] keyValues)
    {
        var product = await unitOfWork.Products.GetByIdAsync(keyValues);
        if (product == null)
        {
            throw new ArgumentException("Product not found.");
        }

        if (entity == null)
        {
            throw new ArgumentException("Invalid input.");
        }
        if (!string.IsNullOrEmpty(entity.Name)) { product.Name = entity.Name; }

        if (entity.CategoryId != 0 && entity.CategoryId != product.CategoryId)
        {
            product.CategoryId = entity.CategoryId;
            product.Category = await unitOfWork.Categories.GetByIdAsync(entity.CategoryId);
        }
        if (entity.ProductStatusId != 0) { product.ProductStatusId = entity.ProductStatusId; }
        if (entity.Balance != 0) { product.Balance = entity.Balance; }
        if (entity.Price != 0) { product.Price = entity.Price; }
        if (!string.IsNullOrEmpty(entity.Description)) { product.Description = entity.Description; }

        await unitOfWork.Products.Update(product);
        await unitOfWork.CompleteAsync();

        return product;
    }
}