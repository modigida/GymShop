using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private async Task<UserResponseDto> MapUserToDto(User user)
    {
        var roles = await unitOfWork.Roles.GetAllAsync();
        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            Role = roles.FirstOrDefault(r => r.Id == user.RoleId).Name
        };
    }



    public async Task<Order> GetEntity(params object[] keyValues)
    {
        return await unitOfWork.Orders.GetByIdAsync(keyValues);
    }

    public async Task<IEnumerable<OrderDto?>> GetAllAsync()
    {
        var orders = await unitOfWork.Orders.GetAllAsync();

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            User = new UserResponseDto
            {
                Id = o.User.Id,
                FirstName = o.User.FirstName,
                LastName = o.User.LastName,
                Email = o.User.Email
            },
            PurchaseDate = o.PurchaseDate,
            OrderStatus = o.OrderStatus,
            TotalPrice = o.TotalPrice,
            OrderProducts = o.OrderProducts?.Select(op => new OrderProductDto
            {
                ProductId = op.ProductId,
                ProductName = op.Product.Name,
                Quantity = op.Quantity,
                CurrentPrice = op.CurrentPrice
            }).ToList() ?? new List<OrderProductDto>()
        }).ToList();
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return null;

        return new OrderDto
        {
            Id = order.Id,
            User = new UserResponseDto
            {
                Id = order.User.Id,
                FirstName = order.User.FirstName,
                LastName = order.User.LastName,
                Email = order.User.Email
            },
            PurchaseDate = order.PurchaseDate,
            OrderStatus = order.OrderStatus,
            TotalPrice = order.TotalPrice,
            OrderProducts = order.OrderProducts?.Select(op => new OrderProductDto
            {
                ProductId = op.ProductId,
                ProductName = op.Product.Name,
                Quantity = op.Quantity,
                CurrentPrice = op.CurrentPrice
            }).ToList() ?? new List<OrderProductDto>()
        };
    }

    public async Task<IEnumerable<OrderDto?>> GetByEmailAsync(string email)
    {
        var orders = await unitOfWork.Orders.GetByEmailAsync(email);

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            User = new UserResponseDto
            {
                Id = o.User.Id,
                FirstName = o.User.FirstName,
                LastName = o.User.LastName,
                Email = o.User.Email
            },
            PurchaseDate = o.PurchaseDate,
            OrderStatus = o.OrderStatus,
            TotalPrice = o.TotalPrice,
            OrderProducts = o.OrderProducts?.Select(op => new OrderProductDto
            {
                ProductId = op.ProductId,
                ProductName = op.Product.Name,
                Quantity = op.Quantity,
                CurrentPrice = op.CurrentPrice
            }).ToList() ?? new List<OrderProductDto>()
        }).ToList();

    }

    public async Task<OrderDto> AddAsync(OrderDto orderDto)
    {
        if (orderDto.User == null || orderDto.PurchaseDate == DateTime.MinValue || orderDto.OrderStatus == null)
        {
            throw new ArgumentException("Invalid input.");
        }

        Order order = new()
        {
            UserId = orderDto.User.Id,
            PurchaseDate = orderDto.PurchaseDate,
            OrderStatusId = orderDto.OrderStatus.Id
        };

        await unitOfWork.Orders.AddAsync(order);
        await unitOfWork.CompleteAsync();

        //entity.User = await unitOfWork.Users.GetByIdAsync(entity.UserId);
        //entity.OrderStatus = await unitOfWork.OrderStatuses.GetByIdAsync(entity.OrderStatusId);
        return orderDto;
    }
    public async Task<OrderDto> Update(OrderDto orderDto, params object[] keyValues)
    {
        var order = await GetEntity(keyValues);
        if (order == null)
        {
            throw new ArgumentException("Order not found.");
        }
        if (orderDto == null)
        {
            throw new ArgumentException("Invalid input.");
        }
        if (orderDto.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be updated after it has been completed.");
        }

        if (orderDto.OrderStatus != null && orderDto.OrderStatus != order.OrderStatus)
        {
            order.OrderStatus = orderDto.OrderStatus;
        }

        await unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();
        return orderDto;
    }

    public async Task Delete(params object[] keyValues)
    {
        var entity = await GetEntity(keyValues);
        if (entity == null)
        {
            throw new ArgumentException("Order not found.");
        }
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be deleted after it has been completed.");
        }
        await unitOfWork.Orders.Delete(entity);
        await unitOfWork.CompleteAsync();
    }
}
