namespace EShop.Domain.Models;

public class ProductType : BaseEntity<Guid>
{
    public string Name { get; set; }
}