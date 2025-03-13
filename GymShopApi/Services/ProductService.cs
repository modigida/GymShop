using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<Product> GetEntity(params object[] keyValues)
    {
        return await unitOfWork.Products.GetByIdAsync(keyValues);
    }
    public async Task<IEnumerable<ProductDto?>> GetAllAsync()
    {
        var products = await unitOfWork.Products.GetAllAsync();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Category = new Category
            {
                Id = p.Category.Id,
                Name = p.Category.Name
            },
            ProductStatus = new ProductStatus
            {
                Id = p.ProductStatus.Id,
                Name = p.ProductStatus.Name
            },
            Balance = p.Balance,
            Price = p.Price,
            Description = p.Description,
            ImageUrl = p.ImageUrl
        }).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = new Category
            {
                Id = product.Category.Id,
                Name = product.Category.Name
            },
            ProductStatus = new ProductStatus
            {
                Id = product.ProductStatus.Id,
                Name = product.ProductStatus.Name
            },
            Balance = product.Balance,
            Price = product.Price,
            Description = product.Description,
            ImageUrl = product.ImageUrl
        };
    }

    public async Task<IEnumerable<ProductDto?>> GetByCategoryAsync(int categoryId)
    {
        var products = await unitOfWork.Products.GetByCategory(categoryId);

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Category = new Category
            {
                Id = p.Category.Id,
                Name = p.Category.Name
            },
            ProductStatus = new ProductStatus
            {
                Id = p.ProductStatus.Id,
                Name = p.ProductStatus.Name
            },
            Balance = p.Balance,
            Price = p.Price,
            Description = p.Description,
            ImageUrl = p.ImageUrl
        }).ToList();
    }

    public async Task<IEnumerable<ProductDto?>> GetByStatusAsync(int statusId)
    {
        var products = await unitOfWork.Products.GetByStatus(statusId);

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Category = new Category
            {
                Id = p.Category.Id,
                Name = p.Category.Name
            },
            ProductStatus = new ProductStatus
            {
                Id = p.ProductStatus.Id,
                Name = p.ProductStatus.Name
            },
            Balance = p.Balance,
            Price = p.Price,
            Description = p.Description,
            ImageUrl = p.ImageUrl
        }).ToList();
    }

    public Task<ProductDto> AddAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }
    public Task<ProductDto> Update(ProductDto entity, params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public Task Delete(params object[] keyValues)
    {
        throw new NotImplementedException();
    }





    //public override async Task<Product> AddAsync(Product entity)
    //{
    //    if (string.IsNullOrEmpty(entity.Name) || entity.Balance <= 0.0 || entity.Price <= 0)
    //    {
    //        throw new ArgumentException("Invalid input.");
    //    }
    //    await unitOfWork.Products.AddAsync(entity);
    //    await unitOfWork.CompleteAsync();

    //    entity.Category = await unitOfWork.Categories.GetByIdAsync(entity.CategoryId);
    //    return entity;
    //}

    //public override async Task<Product> Update(Product entity, params object[] keyValues)
    //{
    //    var product = await unitOfWork.Products.GetByIdAsync(keyValues);
    //    if (product == null)
    //    {
    //        throw new ArgumentException("Product not found.");
    //    }

    //    if (entity == null)
    //    {
    //        throw new ArgumentException("Invalid input.");
    //    }
    //    if (!string.IsNullOrEmpty(entity.Name)) { product.Name = entity.Name; }

    //    if (entity.CategoryId != 0 && entity.CategoryId != product.CategoryId)
    //    {
    //        product.CategoryId = entity.CategoryId;
    //        product.Category = await unitOfWork.Categories.GetByIdAsync(entity.CategoryId);
    //    }
    //    if (entity.ProductStatusId != 0) { product.ProductStatusId = entity.ProductStatusId; }
    //    if (entity.Balance != 0) { product.Balance = entity.Balance; }
    //    if (entity.Price != 0) { product.Price = entity.Price; }
    //    if (!string.IsNullOrEmpty(entity.Description)) { product.Description = entity.Description; }

    //    await unitOfWork.Products.Update(product);
    //    await unitOfWork.CompleteAsync();

    //    return product;
    //}


}