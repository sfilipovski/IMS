using IMS.Domain.DTO.Command;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Domain.Models;
using IMS.Domain.Relationship;
using System.Security.Claims;

namespace IMS.Web.Controllers;


public class ProductsController : Controller
{

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ISupplierService _supplierService;
    private readonly IWarehouseService _warehouseService;

    public ProductsController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService, IWarehouseService warehouseService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _supplierService = supplierService;
        _warehouseService = warehouseService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }

    public IActionResult Create()
    {
        var categories = _categoryService.GetAllCategories();
        var suppliers = _supplierService.GetAllSuppliers();
        ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
        ViewBag.Suppliers = new SelectList(suppliers, "Id", "SupplierName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid) return BadRequest(product);

         _productService.CreateNewProduct(product);
        
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var product = _productService.GetProductById(id);

        if (product == null) return NotFound();

        var categories = _categoryService.GetAllCategories();
        var suppliers = _supplierService.GetAllSuppliers();
        ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
        ViewBag.Suppliers = new SelectList(suppliers, "Id", "SupplierName");
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ProductName,ProductDescription,ProdctSKU,ProductImageUrl,ProductPrice,ProductCategoryId,ProductSupplierId")] Product update)
    {
        if (!ModelState.IsValid) return BadRequest(update);

        _productService.UpdateProduct(id, update);
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: /Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        _productService.DeleteProduct(id);
        return RedirectToAction("Index");
    }

    public IActionResult AddProductInWarehouse(int id)
    {
        var warehouseProduct = _productService.GetSelectedProduct(id);

        if(warehouseProduct == null) return NotFound();

        var warehouses = this._warehouseService.GetAllWarehouses();
        ViewBag.Warehouses = new SelectList(warehouses, "Id", "WarehouseName");
        return View(warehouseProduct);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddProductInWarehouse([Bind("WarehouseProductId,WarehouseId,QuantityInStock,ReorderLimit")] WarehouseProducts wp)
    {
        if (ModelState.IsValid)
        {
            this._productService.AddProductToWarehouse(wp);
            return RedirectToAction("Index");
        }

        return BadRequest(ModelState);
    }

    public IActionResult AddCartProduct(int id)
    {
        var cartProduct = _productService.GetCartProduct(id);
        
        if (cartProduct == null) return NotFound(cartProduct);

        var warehouses = this._productService.GetWarehousesWithProductId(cartProduct.ProductId);
        ViewBag.Warehouses = new SelectList(warehouses, "Id", "WarehouseName");

        return View(cartProduct);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddCartProduct([Bind("ProductId,WarehouseId,Quantity")]AddCartProductDto cp)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _productService.AddCartProduct(cp, userId);

            if (result) return RedirectToAction("Index");

            return View(cp);
        }
        return BadRequest(ModelState);
    }
}
