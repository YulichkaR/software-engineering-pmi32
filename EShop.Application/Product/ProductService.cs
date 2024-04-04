using AutoMapper;
using EShop.Domain.Specification;

namespace EShop.Application.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly string _uploadPath = Path.Combine("wwwroot","images", "products");
    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<GetProductDto> GetProductById(Guid id)
    {
        var product = await _repository.GetBySpecificationAsync(new GetProductDetailsSpecification(id));
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        var productDto = _mapper.Map<GetProductDto>(product);
        return productDto;
    }

    public async Task<List<Domain.Models.Product>> GetProducts(int page, int pageSize)
    {
        return await _repository.GetAllBySpecificationAsync(new GetPagedProductsSpecification(page, pageSize));
    }
    
    public async Task<Domain.Models.Product> CreateProduct(CreateProductDto product)
    {
        var newProduct = _mapper.Map<Domain.Models.Product>(product);
        //file upload
        await UploadImage(product, newProduct);
        
        return await _repository.CreateAsync(newProduct);
    }

    public async Task<Domain.Models.Product> UpdateProduct(Guid id, UpdateProductDto product)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            throw new Exception("Product not found");
        }
        var updatedProduct = _mapper.Map(product, existingProduct);
        if(product.ImgFile is not null)
        {
            await UploadImage(product, updatedProduct);
        }
        await _repository.UpdateAsync(updatedProduct);
        return updatedProduct;
    }

    public Task<int> GetTotalProducts()
    {
        return _repository.CountAsync();
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        await _repository.DeleteAsync(id);
        DeleteProductImage(product);
        return true;
    }

    private async Task UploadImage(CreateProductDto product, Domain.Models.Product newProduct)
    {
        if (product.ImgFile is not null)
        {
            var fileName = $"{Guid.NewGuid()}_{product.ImgFile?.FileName}";
            var filePath = Path.Combine(_uploadPath, fileName);
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
            await using (var fileStream = new FileStream(Path.Combine(filePath), FileMode.Create, FileAccess.Write))
            {
                await product.ImgFile?.CopyToAsync(fileStream)!;
            }
            newProduct.Img = fileName;
        }
    }
    
    private void DeleteProductImage(Domain.Models.Product product)
    {
        var imgPath = Path.Combine(_uploadPath, product.Img);
        if (File.Exists(imgPath))
        {
            File.Delete(imgPath);
        }
    }
}