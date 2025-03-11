using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class RoleService(IUnitOfWork unitOfWork) : GenericService<Role>(unitOfWork)
{
    public async Task<bool> NameExistsAsync(string name)
    {
        var roles = await unitOfWork.Roles.GetAllAsync();
        return roles.Any(r => r.Name == name);
    }
    public override async Task<Role> AddAsync(Role entity)
    {
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Role name is required.");
        }
        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Role with the same name already exists.");
        }

        await unitOfWork.Roles.AddAsync(entity);
        await unitOfWork.CompleteAsync();

        return entity;
    }

    public override async Task<Role> Update(Role entity, params object[] keyValues)
    {
        var role = await GetByIdAsync(keyValues);
        if (role == null)
        {
            throw new ArgumentException("Role not found.");
        }
        if (entity == null || string.IsNullOrEmpty(entity.Name))
        {
            throw new ArgumentException("Role name is required.");
        }

        if (await NameExistsAsync(entity.Name))
        {
            throw new ArgumentException("Role with the same name already exists.");
        }

        role.Name = entity.Name;

        await unitOfWork.Roles.Update(role);
        await unitOfWork.CompleteAsync();

        return role;
    }
}
