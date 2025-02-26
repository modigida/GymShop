using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public class CategoryService(IUnitOfWork unitOfWork) : GenericService<Category>(unitOfWork)
{
    public async Task<bool> NameExistsAsync(string name)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        return categories.Any(c => c.Name == name);
    }
    public override async Task<Category> AddAsync(Category entity)
    {
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Category name is required.");
        }
        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Category with the same name already exists.");
        }

        return await base.AddAsync(entity);
    }
    public override async Task<Category> Update(int id, Category entity)
    {
        var category = await GetByIdAsync(id);
        if (category == null)
        {
            throw new ArgumentException("Category not found.");
        }
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Category name is required.");
        }

        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Category with the same name already exists.");
        }

        category.Name = entity.Name;
        return await base.Update(category.Id, category);
    }
}
