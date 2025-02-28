using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public abstract class GenericService<T>(IUnitOfWork unitOfWork) : IGenericService<T> where T : class
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IGenericRepository<T> _repository = unitOfWork.GetRepository<T>();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual async Task<T?> GetByIdAsync(object id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public abstract Task<T> AddAsync(T entity);

    public abstract Task<T> Update(int id, T entity);

    public virtual async Task Delete(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            throw new ArgumentException("Entity not found.");
        }
        await _repository.Delete(entity);
        await _unitOfWork.CompleteAsync();
    }
}
