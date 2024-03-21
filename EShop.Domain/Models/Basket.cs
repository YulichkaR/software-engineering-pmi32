using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Basket : BaseEntity<Guid>
{
    public decimal TotalPrice { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    [JsonIgnore]
    public ICollection<Product> Items { get; set; }
}