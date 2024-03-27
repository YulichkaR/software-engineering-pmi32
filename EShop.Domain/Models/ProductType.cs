namespace EShop.Domain.Models;

public class ProductType : IBaseEntity<Guid>
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}