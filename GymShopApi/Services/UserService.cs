using GymShopApi.Entities;
using GymShopApi.Repositories.Interfaces;

namespace GymShopApi.Services;
public class UserService(IUnitOfWork unitOfWork) : GenericService<User>(unitOfWork)
{
    public async Task<bool> PhoneExistsAsync(string name)
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return users.Any(u => u.Phone == name);
    }
    public async Task<bool> EmailExistsAsync(string name)
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return users.Any(u => u.Email == name);
    }
    public override Task<User> AddAsync(User entity)
    {
        // TODO
        throw new NotImplementedException();
    }

    public override async Task<User> Update(int id, User entity)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }
        if (entity == null)
        {
            throw new ArgumentException("Invalid input.");
        }

        if (!string.IsNullOrEmpty(entity.FirstName)) { user.FirstName = entity.FirstName; }
        if (!string.IsNullOrEmpty(entity.LastName)) { user.LastName = entity.LastName; }

        if (entity.RoleId != 0 && entity.RoleId != user.RoleId)
        {
            user.RoleId = entity.RoleId;
            user.Role = await _unitOfWork.Roles.GetByIdAsync(entity.RoleId);
        }

        // TODO: manage hash and salt 
        if (!string.IsNullOrEmpty(entity.PasswordHash)) { user.PasswordHash = entity.PasswordHash; }
        if (!string.IsNullOrEmpty(entity.PasswordSalt)) { user.PasswordSalt = entity.PasswordSalt; }

        if (!string.IsNullOrEmpty(entity.Email) && await EmailExistsAsync(entity.Email))
        {
            user.Email = entity.Email;
        }
        else if (!string.IsNullOrEmpty(entity.Email) && !await EmailExistsAsync(entity.Email) && entity.Email != user.Email)
        {
            throw new ArgumentException("Email already exists.");
        }

        if (!string.IsNullOrEmpty(entity.Phone) && await PhoneExistsAsync(entity.Phone))
        {
            user.Phone = entity.Phone;
        }
        else if (!string.IsNullOrEmpty(entity.Phone) && !await PhoneExistsAsync(entity.Phone) && entity.Phone != user.Phone)
        {
            throw new ArgumentException("Phone already exists.");
        }

        if (!string.IsNullOrEmpty(entity.Address)) { user.Address = entity.Address; }

        await _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();

        return user;
    }
}
