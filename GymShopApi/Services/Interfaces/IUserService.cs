﻿using GymShopApi.DTOs;
using GymShopApi.Entities;

namespace GymShopApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto?>> GetAllAsync();
        Task<IEnumerable<UserResponseDto?>> GetAllCustomersAsync();
        Task<UserResponseDto?> GetByIdAsync(params object[] keyValues);
        Task<UserResponseDto> Update(UserCreateDto entity, params object[] keyValues);
        Task Delete(params object[] keyValues);
        Task<UserResponseDto> RegisterUserAsync(UserCreateDto dto);
        Task<string?> LoginUserAsync(UserLoginDto dto);
        Task<bool> ValidatePasswordAsync(string email, string password);
    }
}