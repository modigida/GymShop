using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Hasher;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;
using System.Data;
using GymShopApi.JWT;

namespace GymShopApi.Services;
public class UserService(IUnitOfWork unitOfWork, JwtService jwtService) : IUserService
{
    public async Task<IEnumerable<UserResponseDto?>> GetAllAsync()
    {
        var users = await unitOfWork.Users.GetAllAsync();
        List<UserResponseDto>? userResponseDtos = new List<UserResponseDto>();
        foreach (var user in users)
        {
            userResponseDtos.Add(new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role
            });
        }
        return userResponseDtos;
    }
    public async Task<UserResponseDto?> GetByIdAsync(params object[] keyValues)
    {
        var user = await unitOfWork.Users.GetByIdAsync(keyValues);
        if (user == null)
        {
            return null;
        }
        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            Role = user.Role
        };
    }
    public async Task<UserResponseDto> Update(UserCreateDto entity, params object[] keyValues)
    {
        var user = await unitOfWork.Users.GetByIdAsync(keyValues);
        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }

        if (!string.IsNullOrEmpty(entity.FirstName) && entity.FirstName != user.FirstName)
        {
            user.FirstName = entity.FirstName;
        }

        if (!string.IsNullOrEmpty(entity.LastName) && entity.LastName != user.LastName)
        {
            user.LastName = entity.LastName;
        }

        //if (!string.IsNullOrEmpty(entity.Password))
        //{
        //    PasswordHasher.CreatePasswordHash(entity.Password, out string newHash, out string newSalt);
        //    user.PasswordHash = newHash;
        //    user.PasswordSalt = newSalt;
        //}

        if (!string.IsNullOrEmpty(entity.Email) && entity.Email != user.Email)
        {
            if (await EmailExistsAsync(entity.Email))
            {
                throw new ArgumentException("Email already exists.");
            }
            user.Email = entity.Email;
        }
        
        if (!string.IsNullOrEmpty(entity.Phone) && entity.Phone != user.Phone)
        {
            if (await EmailExistsAsync(entity.Phone))
            {
                throw new ArgumentException("Phone already exists.");
            }
            user.Phone = entity.Phone;
        }

        if (!string.IsNullOrEmpty(entity.Address) && entity.Address != user.Address)
        {
            user.Address = entity.Address;
        }

        if (entity.RoleId != 0 && entity.RoleId != user.RoleId)
        {
            user.RoleId = entity.RoleId;
        }

        await unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();

        var role = await unitOfWork.Roles.GetByIdAsync(user.RoleId);
        return new UserResponseDto
        {
            Id = user.Id,
            Address = user.Address,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Role = role
        };
    }

    public async Task<UserResponseDto> RegisterUserAsync(UserCreateDto dto)
    {
        if (await EmailExistsAsync(dto.Email))
        {
            throw new ArgumentException("Email already exists.");
        }
        if (await PhoneExistsAsync(dto.Phone))
        {
            throw new ArgumentException("Phone already exists.");
        }

        PasswordHasher.CreatePasswordHash(dto.Password, out var passwordHash, out var passwordSalt);
        
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            RoleId = dto.RoleId,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.CompleteAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            Role = user.Role
        };
    }

    public async Task<string?> LoginUserAsync(UserLoginDto dto)
    {
        //var user = await unitOfWork.Users.FindAsync(u => u.Email == dto.Email);
        var user = (await unitOfWork.Users.GetAllAsync()).FirstOrDefault(u => u.Email == dto.Email);
        user.Role = await unitOfWork.Roles.GetByIdAsync(user.RoleId);
        if (user == null)
        {
            return null;
        }
        var isPasswordValid = PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt);
        if (!isPasswordValid)
        {
            return null;
        }

        if (user.Role == null)
        {
            return null;
        }
        return jwtService.GenerateToken(user.Id, user.Email, user.Role.Name);
    }
    public async Task<bool> PhoneExistsAsync(string name)
    {
        //return await _unitOfWork.Users.AnyAsync(u => u.Email == name);
        var users = await unitOfWork.Users.GetAllAsync();
        return users.Any(u => u.Phone == name);
    }
    public async Task<bool> EmailExistsAsync(string name)
    {
        //return await _unitOfWork.Users.AnyAsync(u => u.Email == name);
        var users = await unitOfWork.Users.GetAllAsync();
        return users.Any(u => u.Email == name);
    }

    public async Task Delete(params object[] keyValues)
    {
        var user = await unitOfWork.Users.GetByIdAsync(keyValues);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        await unitOfWork.Users.Delete(user);
        await unitOfWork.CompleteAsync();
    }
}
