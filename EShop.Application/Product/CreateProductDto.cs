

using Microsoft.AspNetCore.Http;

namespace EShop.Application.Product;

public record CreateProductDto
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string? Img { get; set; }
    public IFormFile? ImgFile { get; set; }
    public Guid ProductTypeId { get; set; }
};