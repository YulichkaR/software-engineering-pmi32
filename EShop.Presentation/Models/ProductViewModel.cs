using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShop.Presentation.Models;

public class ProductViewModel
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string? Img { get; set; }
    public string ProductType { get; set; }
}