namespace EShop.Application.Product;

public interface IProductService
{
    Task<GetProductDto> GetProductById(Guid id);
    Task<List<Domain.Models.Product>> GetProducts(int page, int pageSize);
    Task<Domain.Models.Product> CreateProduct(CreateProductDto product);
    Task<Domain.Models.Product> UpdateProduct(Guid id, UpdateProductDto product);
    Task<int> GetTotalProducts();
    Task<bool> DeleteProduct(Guid id);
}