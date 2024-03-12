using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Basket
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    [JsonIgnore]
    public ICollection<Item> Items { get; set; }
}