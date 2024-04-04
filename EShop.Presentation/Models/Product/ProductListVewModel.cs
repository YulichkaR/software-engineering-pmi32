namespace EShop.Presentation.Models.Product;

public class ProductListVewModel
{
    public List<Domain.Models.Product> Products { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; } 
}