using IMS.Domain.DTO.Command;
using IMS.Domain.Models;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Web.Controllers;

public class WarehousesController : Controller
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    public IActionResult Index()
    {
        return View(this._warehouseService.GetAllWarehouses());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("WarehouseName,WarehouseAddress,WarehouseCity,WarehouseCountry")] Warehouse warehouse)
    {
        if (ModelState.IsValid)
        {
            this._warehouseService.CreateNewWarehouse(warehouse); 
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        var warehouse = this._warehouseService.GetWarehouseById(id);

        if (warehouse == null) return NotFound();

        return View(warehouse);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("WarehouseName,WarehouseAddress,WarehouseCity,WarehouseCountry")] Warehouse update)
    {
        if (ModelState.IsValid)
        {
            this._warehouseService.UpdateWarehouse(id, update);
            return RedirectToAction("Index");
        }

        return View("Edit");
    }

    public IActionResult Delete(int id)
    {
        var warehouse = this._warehouseService.GetWarehouseById(id);

        if (warehouse == null) return NotFound();

        return View(warehouse);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        this._warehouseService.DeleteWarehouse(id);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteProduct(int id)
    {
        var warehouseProduct = this._warehouseService.GetById(id);

        if (warehouseProduct == null) return NotFound(warehouseProduct);

        return View(warehouseProduct);
    }

    [HttpPost, ActionName("DeleteProduct")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteProductConfirmed(int id)
    {
        this._warehouseService.DeleteWarehouseProduct(id);
        return RedirectToAction("Index");
    }

    public IActionResult GetAllWarehouseProducts()
    {
        return View(this._warehouseService.GetAllWarehouseProducts());
    }

    public IActionResult GetWarehouseProducts(int id)
    {
        return View("GetAllWarehouseProducts", this._warehouseService.GetWarehouseProducts(id));
    }

    public IActionResult Reorder(int id)
    {
        var wp = this._warehouseService.GetById(id);
        if (wp == null) return NotFound(wp);

        ReorderQuantityDto dto = new ReorderQuantityDto
        {
            Id = wp.Id,
            ProductId = wp.WarehouseProductId,
            WarehouseId = wp.WarehouseId,
            ReorderQuantity = 1
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Reorder(ReorderQuantityDto dto)
    {
        if (ModelState.IsValid)
        {
            if(this._warehouseService.ReorderQuantity(dto.WarehouseId, dto.ProductId, dto.ReorderQuantity))
                return RedirectToAction("GetAllWarehouseProducts");

            return View(dto);
        }
        return View(ModelState);
    }

}
