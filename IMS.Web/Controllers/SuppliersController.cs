using IMS.Domain.Models;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Web.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            return View(this._supplierService.GetAllSuppliers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SupplierName,SupplierAddress,SupplierPhone")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                this._supplierService.CreateNewSupplier(supplier);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id) 
        {
            var supplier = this._supplierService.GetSupplierById(id);

            if (supplier == null) return NotFound();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("SupplierName,SupplierAddress,SupplierPhone")] Supplier update)
        {
            if(ModelState.IsValid)
            {
                this._supplierService.UpdateSupplier(id, update);
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        public IActionResult Delete(int id)
        {
            var supplier = this._supplierService.GetSupplierById(id);

            if(supplier == null) return NotFound();

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            this._supplierService.DeleteSupplier(id);
            return RedirectToAction("Index");
        }
    }
}
