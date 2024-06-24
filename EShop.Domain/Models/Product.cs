using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Product : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = null!;
    public string Img { get; set; } = null!;
    public Guid ProductTypeId { get; set; }
    [ForeignKey(nameof(Models.Color))]
    
    public Guid ProductColorId { get; set; }

    [JsonIgnore]
    public Color Color { get; set; } = null!;
    [JsonIgnore]
    public ProductType Type { get; set; } = null!;
    [JsonIgnore]
    public ICollection<BasketItem> Baskets { get; set; } = [];
    [JsonIgnore]
    public ICollection<ProductLike> Likes { get; set; } = [];
}