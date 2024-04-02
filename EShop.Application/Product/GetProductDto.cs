namespace EShop.Application.Product;

public class GetProductDto
{
    public Guid Id { get; set; }
    public decimal Price{ get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = null!;
    public string Img { get; set; } = null!;
    public Domain.Models.ProductType Type { get; set; } = null!;
}