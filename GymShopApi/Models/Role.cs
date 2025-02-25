﻿using System.ComponentModel.DataAnnotations;

namespace GymShopApi.Models;
public class Role
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [MaxLength(50)] 
    public string Name { get; set; } = string.Empty;  // Admin, User
}

