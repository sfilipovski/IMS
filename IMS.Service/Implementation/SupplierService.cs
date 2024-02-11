using IMS.Domain.Models;
using IMS.Repository.Interface;
using IMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Service.Implementation;

public class SupplierService : ISupplierService
{
    private readonly IRepository<Supplier> _supplierRepository;

    public SupplierService(IRepository<Supplier> supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public void CreateNewSupplier(Supplier supplier)
    {
        this._supplierRepository.Create(supplier);
    }

    public void DeleteSupplier(int? id)
    {
        var supplier = _supplierRepository.Get(id);
        this._supplierRepository.Delete(supplier);
    }

    public List<Supplier> GetAllSuppliers()
    {
        return this._supplierRepository.GetAll().ToList();
    }

    public Supplier GetSupplierById(int? id)
    {
        return this._supplierRepository.Get(id);
    }

    public void UpdateSupplier(int id, Supplier supplier)
    {
        var s = this._supplierRepository.Get(id);
        s.SupplierPhone = supplier.SupplierPhone;
        s.SupplierName = supplier.SupplierName;
        s.SupplierAddress = supplier.SupplierAddress;

        this._supplierRepository.Update(s);
    }
}
