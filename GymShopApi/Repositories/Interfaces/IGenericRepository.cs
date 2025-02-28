namespace GymShopApi.Repositories.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(params object[] keyValues);
    Task AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
