using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Product : BaseEntity<Guid>
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public Guid ProductTypeId { get; set; }
    
    [JsonIgnore]
    public ProductType Type { get; set; }
    [JsonIgnore]
    public ICollection<Basket> Baskets { get; set; }
}