using EShop.Domain.Enums;

namespace EShop.Domain.Models;

public class Order : BaseEntity<Guid>
{
    public DateTimeOffset OrderTime { get; set; }
    public Status Status { get; set; }
    public string Address { get; set; }
    public Guid BasketId { get; set; }
    public Guid UserId { get; set; }
    public Basket Basket { get; set; }
    public User User { get; set; }
}