namespace EShop.Application.Product;

public class GetProductDto
{
    public Guid Id { get; set; }
    public decimal Price{ get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public Domain.Models.ProductType Type { get; set; }
}