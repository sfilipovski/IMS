using IMS.Domain.Models;
using IMS.Service.Interface;
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
}
