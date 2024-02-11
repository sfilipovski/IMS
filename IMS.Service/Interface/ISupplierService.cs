using IMS.Domain.Models;

namespace IMS.Service.Interface;

public interface ISupplierService
{
    List<Supplier> GetAllSuppliers();
    Supplier GetSupplierById(int? id);
    void CreateNewSupplier(Supplier supplier);
    void UpdateSupplier(int id, Supplier supplier);
    void DeleteSupplier(int? id);
}
