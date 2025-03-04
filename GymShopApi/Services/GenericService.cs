using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public abstract class GenericService<T>(IUnitOfWork unitOfWork) : IGenericService<T> where T : class
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IGenericRepository<T> _repository = unitOfWork.GetRepository<T>();

    public virtual async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual async Task<T?> GetByIdAsync(params object[] keyValues)
    {
        return await _repository.GetByIdAsync(keyValues);
    }

    public abstract Task<T> AddAsync(T entity);
    public abstract Task<T> Update(T entity, params object[] keyValues);

    public virtual async Task Delete(params object[] keyValues)
    {
        var entity = await GetByIdAsync(keyValues);
        if (entity == null)
        {
            throw new ArgumentException("Entity not found.");
        }
        await _repository.Delete(entity);
        await _unitOfWork.CompleteAsync();
    }
}
