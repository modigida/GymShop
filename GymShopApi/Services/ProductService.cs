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

    public async Task<ProductDto> AddAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            CategoryId = productDto.Category.Id,
            ProductStatusId = productDto.ProductStatus.Id,
            Balance = productDto.Balance,
            Price = productDto.Price,
            Description = productDto.Description,
            ImageUrl = productDto.ImageUrl
        };

        await unitOfWork.Products.AddAsync(product);
        await unitOfWork.CompleteAsync();

        var category = await unitOfWork.Categories.GetByIdAsync(product.CategoryId);
        var status = await unitOfWork.ProductStatuses.GetByIdAsync(product.ProductStatusId);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = category,
            ProductStatus = status,
            Balance = product.Balance,
            Price = product.Price,
            Description = product.Description,
            ImageUrl = product.ImageUrl
        };
    }
    public async Task<ProductDto> Update(ProductDto productDto, params object[] keyValues)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(keyValues);
        if (entity == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }
        bool hasChanges = false;

        if (!string.IsNullOrEmpty(productDto.Name) && productDto.Name != entity.Name)
        {
            entity.Name = productDto.Name;
            hasChanges = true;
        }

        if (productDto.Category?.Id != null && productDto.Category.Id != entity.CategoryId)
        {
            entity.CategoryId = productDto.Category.Id;
            hasChanges = true;
        }

        if (productDto.ProductStatus?.Id != null && productDto.ProductStatus.Id != entity.ProductStatusId)
        {
            entity.ProductStatusId = productDto.ProductStatus.Id;
            hasChanges = true;
        }

        if (productDto.Balance != entity.Balance)
        {
            entity.Balance = productDto.Balance;
            hasChanges = true;
        }

        if (productDto.Price != entity.Price)
        {
            entity.Price = productDto.Price;
            hasChanges = true;
        }

        if (!string.IsNullOrEmpty(productDto.Description) && productDto.Description != entity.Description)
        {
            entity.Description = productDto.Description;
            hasChanges = true;
        }

        if (!string.IsNullOrEmpty(productDto.ImageUrl) && productDto.ImageUrl != entity.ImageUrl)
        {
            entity.ImageUrl = productDto.ImageUrl;
            hasChanges = true;
        }

        if (!hasChanges)
        {
            var existingCategory = await unitOfWork.Categories.GetByIdAsync(entity.CategoryId);
            var existingStatus = await unitOfWork.ProductStatuses.GetByIdAsync(entity.ProductStatusId);

            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Category = existingCategory,
                ProductStatus = existingStatus,
                Balance = entity.Balance,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl
            };
        }

        await unitOfWork.Products.Update(entity);
        await unitOfWork.CompleteAsync();

        var category = await unitOfWork.Categories.GetByIdAsync(entity.CategoryId);
        var status = await unitOfWork.ProductStatuses.GetByIdAsync(entity.ProductStatusId);

        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Category = category,
            ProductStatus = status,
            Balance = entity.Balance,
            Price = entity.Price,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl
        };
    }

    public async Task Delete(params object[] keyValues)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(keyValues);
        if (entity == null)
        {
            throw new ArgumentException("Entity not found.");
        }

        bool isUsedInOrders = await unitOfWork.OrderProducts.AnyAsync((int)keyValues[0]);

        if (isUsedInOrders)
        {
            throw new InvalidOperationException("Produkten kan inte tas bort eftersom den är kopplad till en order.");
        }

        await unitOfWork.Products.Delete(entity);
        await unitOfWork.CompleteAsync();
    }

}