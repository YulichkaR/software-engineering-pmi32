﻿namespace EShop.Domain.Models;

public class BasketItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid BasketId { get; set; }
    public int Quantity { get; set; }
    public Basket Basket { get; set; } = null!;
    public Product Product { get; set; } = null!;
}