﻿namespace GymShopApi.Models;
public class Status
{
    public int Id { get; set; }
    public required string Name { get; set; } // Pending, Processing, Completed
}
