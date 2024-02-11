using IMS.Domain.DTO.Command;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Web.Controllers;


public class ProductsController : Controller
{

    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ProductName,ProductDescription,ProductSKU,ProductImageUrl,ProductPrice")] CreateProductDto product)
    {
        if (!ModelState.IsValid) return View(product);

        _productService.CreateNewProduct(product);
        return RedirectToAction("Index");
    }
}
