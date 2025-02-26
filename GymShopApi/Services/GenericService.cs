using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public class GenericService<T>(IUnitOfWork unitOfWork) : IGenericService<T> where T : class
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

    public virtual async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
        return entity;
    }

    public virtual async Task<T> Update(int id, T entity)
    {
        await _repository.Update(entity);
        await _unitOfWork.CompleteAsync();
        return entity;
    }

    public virtual async Task Delete(T entity)
    {
        await _repository.Delete(entity);
        await _unitOfWork.CompleteAsync();
    }
}
