using GymShopBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartService
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

        OnCartUpdated?.Invoke();
    }

    public void RemoveFromCart(OrderProduct item)
    {
        CartItems.Remove(item);
        OnCartUpdated?.Invoke();
    }

    public double GetTotalPrice()
    {
        return CartItems.Sum(p => p.CurrentPrice * p.Quantity);
    }
}