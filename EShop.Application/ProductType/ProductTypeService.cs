using AutoMapper;

namespace EShop.Application.ProductType;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductTypeService(IProductTypeRepository productTypeRepository, IMapper mapper)
    {
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }
    public async Task<List<Domain.Models.ProductType>> GetProductTypes()
    {
        return await _productTypeRepository.GetAll();
    }

    public async Task<Domain.Models.ProductType> CreateProductType(CreateProductTypeDto productType)
    {
        var newProductType = _mapper.Map<Domain.Models.ProductType>(productType);
        return await _productTypeRepository.Create(newProductType);
    }

    public async Task Delete(Guid id)
    {
        await _productTypeRepository.Delete(id);
    }
}