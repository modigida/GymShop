using GymShopApi.Entities;
using GymShopApi.Hasher;
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
    public override async Task<User> AddAsync(User entity)
    {
        // TODO: create user with role "admin" can only be made by an admin. Offline can only create users of type
        // customer, this will be automatically, customers can´t choose role
        if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || entity.RoleId == 0 ||
            string.IsNullOrEmpty(entity.Email) || string.IsNullOrEmpty(entity.Phone) ||
            string.IsNullOrEmpty(entity.Address))
        {
            throw new ArgumentException("Invalid input.");
        }
        // TODO delete when Hashing is implemented
        if (string.IsNullOrEmpty(entity.PasswordHash)) { entity.PasswordHash = "TestHash"; }
        if (string.IsNullOrEmpty(entity.PasswordSalt)) { entity.PasswordSalt = "TestSalt"; }
        
        await _unitOfWork.Users.AddAsync(entity);
        await _unitOfWork.CompleteAsync();

        entity.Role = await _unitOfWork.Roles.GetByIdAsync(entity.RoleId);
        return entity;
    }


    public override async Task<User> Update(object id, User entity)
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

        // TODO: to make a user "admin" you need to be logged in as admin
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

    public async Task Delete(Guid id)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        await _unitOfWork.Users.Delete(user);
        await _unitOfWork.CompleteAsync();
    }
}
