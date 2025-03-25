using GymShopBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymShopBlazor.ApiService;

public class CartService(ProductService productService)
{
    public List<OrderProduct> CartItems { get; private set; } = new List<OrderProduct>();

    public event Action? OnCartUpdated;

    public void AddToCart(Product product)
    {
        var existingItem = CartItems.FirstOrDefault(p => p.ProductId == product.Id);
        if (existingItem != null)
        {
            if (existingItem.Quantity < product.Balance)
            {
                existingItem.Quantity++;
            }
        }
        else if (product.Balance > 0)
        {
            CartItems.Add(new OrderProduct
            {
                ProductId = product.Id,
                ProductName = product.Name,
                CurrentPrice = product.Price ?? 0.0,
                Quantity = 1,
                ImageUrl = product.ImageUrl
            });
        }
        else
        {
            return;
        }

        NotifyCartUpdated();
    }

    public void RemoveFromCart(OrderProduct item)
    {
        var existingItem = CartItems.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity--;
            if (existingItem.Quantity <= 0)
            {
                CartItems.Remove(existingItem);
            }
        }

        NotifyCartUpdated();
    }

    public async Task IncreaseQuantity(OrderProduct item)
    {
        var existingItem = CartItems.FirstOrDefault(p => p.ProductId == item.ProductId);
        var product = await productService.GetById(item.ProductId);
        if (existingItem != null && existingItem.Quantity < 99 && existingItem.Quantity < product.Balance)
        {
            existingItem.Quantity++;
            NotifyCartUpdated();
        }
    }
    public void DecreaseQuantity(OrderProduct item)
    {
        var existingItem = CartItems.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (existingItem != null && existingItem.Quantity > 1)
        {
            existingItem.Quantity--;
        }
        else
        {
            CartItems.Remove(existingItem);
        }
        NotifyCartUpdated();
    }

    private void NotifyCartUpdated()
    {
        OnCartUpdated?.Invoke();
    }
}