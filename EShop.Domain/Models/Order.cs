using EShop.Domain.Enums;

namespace EShop.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset OrderTime { get; set; }
    public Status Type { get; set; }
    public string Address { get; set; }
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}