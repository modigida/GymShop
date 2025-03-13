using GymShopApi.DTOs;
using GymShopApi.Entities;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;

namespace GymShopApi.Services;
public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Order> GetEntity(params object[] keyValues)
    {
        return await unitOfWork.Orders.GetByIdAsync(keyValues);
    }

    public async Task<IEnumerable<OrderResponseDto?>> GetAllAsync()
    {
        var orders = await unitOfWork.Orders.GetAllAsync();

        return orders.Select(o => new OrderResponseDto
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

    public async Task<OrderResponseDto?> GetByIdAsync(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        if (order == null) return null;

        return new OrderResponseDto
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

    public async Task<IEnumerable<OrderResponseDto?>> GetByEmailAsync(string email)
    {
        var orders = await unitOfWork.Orders.GetByEmailAsync(email);
        if (!orders.Any()) return new List<OrderResponseDto?>();

        return orders.Select(o => new OrderResponseDto
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

    public async Task<OrderResponseDto> AddAsync(OrderCreateDto entity)
    {
        if (entity.User == null || entity.PurchaseDate == DateTime.MinValue || entity.OrderStatus == null)
        {
            throw new ArgumentException("Invalid input.");
        }

        Order order = new()
        {
            UserId = entity.User.Id,
            PurchaseDate = entity.PurchaseDate,
            OrderStatusId = entity.OrderStatus.Id,
            TotalPrice = entity.OrderProducts?.Sum(op => op.CurrentPrice * op.Quantity) ?? 0
        };

        await unitOfWork.Orders.AddAsync(order);
        await unitOfWork.CompleteAsync();

        List<OrderProduct> orderProducts = new();

        if (entity.OrderProducts != null && entity.OrderProducts.Any())
        {
            var productIds = entity.OrderProducts.Select(op => op.ProductId).ToList();
            var products = await unitOfWork.Products.GetByIdsAsync(productIds);

            orderProducts = entity.OrderProducts.Select(op =>
            {
                var product = products.FirstOrDefault(p => p.Id == op.ProductId);
                return new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    CurrentPrice = op.CurrentPrice,
                    Product = product
                };
            }).ToList();

            order.OrderProducts = orderProducts;

            await unitOfWork.OrderProducts.AddRangeAsync(order.OrderProducts);
            await unitOfWork.CompleteAsync();
        }

        return new OrderResponseDto
        {
            Id = order.Id,
            User = entity.User,
            PurchaseDate = order.PurchaseDate,
            OrderStatus = entity.OrderStatus,
            TotalPrice = order.TotalPrice,
            OrderProducts = order.OrderProducts?.Select(op => new OrderProductDto
            {
                ProductId = op.ProductId,
                Quantity = op.Quantity,
                CurrentPrice = op.CurrentPrice,
                ProductName = op.Product?.Name
            }).ToList() ?? new List<OrderProductDto>()
        };
    }
    public async Task<OrderResponseDto> Update(OrderCreateDto entity, params object[] keyValues)
    {
        var order = await GetEntity(keyValues);
        if (order == null)
        {
            throw new ArgumentException("Order not found.");
        }
        if (entity == null)
        {
            throw new ArgumentException("Invalid input.");
        }
        if (entity.OrderStatus?.Name == "Completed")
        {
            throw new InvalidOperationException("Order cannot be updated after it has been completed.");
        }

        if (entity.OrderStatus != null && entity.OrderStatus.Id != order.OrderStatusId)
        {
            var existingOrderStatus = await unitOfWork.OrderStatuses.GetByIdAsync(entity.OrderStatus.Id);
            if (existingOrderStatus != null)
            {
                order.OrderStatus = existingOrderStatus;
            }
        }

        if (entity.OrderProducts != null && entity.OrderProducts.Any())
        {
            var existingProducts = await unitOfWork.OrderProducts.GetByOrderIdAsync(order.Id);
            await unitOfWork.OrderProducts.DeleteRangeAsync(existingProducts);

            var newOrderProducts = entity.OrderProducts.Select(op => new OrderProduct
            {
                OrderId = order.Id,
                ProductId = op.ProductId,
                Quantity = op.Quantity,
                CurrentPrice = op.CurrentPrice
            }).ToList();

            await unitOfWork.OrderProducts.AddRangeAsync(newOrderProducts);

            order.OrderProducts = newOrderProducts;
        }

        order.TotalPrice = order.OrderProducts?.Sum(op => op.Quantity * op.CurrentPrice) ?? 0;

        await unitOfWork.Orders.Update(order);
        await unitOfWork.CompleteAsync();

        return new OrderResponseDto
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
