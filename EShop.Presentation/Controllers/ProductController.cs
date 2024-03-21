using EShop.Application.Product;
using EShop.Application.ProductType;
using EShop.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductTypeService _productTypeService;

    public ProductController(IProductService productService, IProductTypeService productTypeService)
    {
        _productService = productService;
        _productTypeService = productTypeService;
    }
    // GET
    public async Task<IActionResult> Index(int page = 1, int pageSize = 2)
    {
        var products = await _productService.GetProducts(page,pageSize);
        var productCount = await _productService.GetTotalProducts();
        var viewModel = new ProductListVewModel
        {
            Products = products,
            PageNumber = page,
            PageSize = pageSize,
            TotalItems = productCount
        };
        return View(viewModel);
    }
    //GET PRODUCT 
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productService.GetProductById(id);
        var productVm = new ProductViewModel
        {
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description,
            Img = product.Img,
            ProductType = product.Type.Name
        };
        return View(productVm);
    }
    //Create - page
    public async Task<IActionResult> Create()
    {
        var model = new CreateProductViewModel();
        var productTypes = await _productTypeService.GetProductTypes();
        model.ProductTypes = productTypes.ConvertAll(pt => new SelectListItem
        {
            Value = pt.Id.ToString(),
            Text = pt.Name
        });

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Create));
        }

        var img = HttpContext.Request.Form.Files[0];
        var productDto = new CreateProductDto
        {
            Price = model.Price,
            Quantity = model.Quantity,
            Description = model.Description,
            ProductTypeId = model.ProductTypeId,
            Img = img.FileName,
            ImgFile = img
        };
        var product = await _productService.CreateProduct(productDto);
        return RedirectToAction("Index");
    }
}