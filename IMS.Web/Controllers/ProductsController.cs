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
        if (!ModelState.IsValid) return BadRequest(product);

        _productService.CreateNewProduct(product);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var product = _productService.GetProductById(id);

        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ProductName,ProductDescription,ProductSKU,ProductImageUrl,ProductPrice")] CreateProductDto update)
    {
        if (!ModelState.IsValid) return BadRequest(update);

        _productService.UpdateProduct(id, update);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _productService.DeleteProduct(id);
        return RedirectToAction("Index");
    }
}
