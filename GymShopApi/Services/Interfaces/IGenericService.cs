using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services.Interfaces;
public interface IGenericService<T> where T : class
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(params object[] keyValues);
    Task<T> AddAsync(T entity);
    Task<T> Update(T entity, params object[] keyValues);
    Task Delete(params object[] keyValues);
}
