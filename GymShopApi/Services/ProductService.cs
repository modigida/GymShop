using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class ProductService(IUnitOfWork unitOfWork) : GenericService<Product>(unitOfWork)
{
    public override Task<Product> AddAsync(Product entity)
    {
        // TODO
        throw new NotImplementedException();
    }

    public override async Task<Product> Update(int id, Product entity)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new ArgumentException("Product not found.");
        }

        if (entity == null)
        {
            throw new ArgumentException("Invalid input.");
        }
        if (!string.IsNullOrEmpty(entity.Name)) { product.Name = entity.Name; }
        if (entity.CategoryId != 0) { product.CategoryId = entity.CategoryId; }
        if (entity.ProductStatusId != 0) { product.ProductStatusId = entity.ProductStatusId; }
        if (entity.Balance != 0) { product.Balance = entity.Balance; }
        if (entity.Price != 0) { product.Price = entity.Price; }
        if (!string.IsNullOrEmpty(entity.Description)) { product.Description = entity.Description; }

        await _unitOfWork.Products.Update(product);
        await _unitOfWork.CompleteAsync();

        return product;
    }
}