using GymShopApi.Entities;

namespace GymShopApi.Repositories.Interfaces;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product?>> GetByCategory(int categoryId);
    Task<IEnumerable<Product?>> GetByStatus(int statusId);
}
