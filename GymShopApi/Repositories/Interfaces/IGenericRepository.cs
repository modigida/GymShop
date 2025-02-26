namespace GymShopApi.Repositories.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
