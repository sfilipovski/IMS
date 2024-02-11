using IMS.Domain.Models;
using IMS.Repository.Interface;
using IMS.Service.Interface;

namespace IMS.Service.Implementation;

public class WarehouseService : IWarehouseService
{
    private readonly IRepository<Warehouse> _warehouseRepository;

    public WarehouseService(IRepository<Warehouse> warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public void CreateNewWarehouse(Warehouse newWarehouse)
    {
        this._warehouseRepository.Create(newWarehouse);
    }

    public void DeleteWarehouse(int id)
    {
        var w = this._warehouseRepository.Get(id);
        this._warehouseRepository.Delete(w);
    }

    public List<Warehouse> GetAllWarehouses()
    {
        return this._warehouseRepository.GetAll().ToList();
    }

    public Warehouse GetWarehouseById(int? id)
    {
        return this._warehouseRepository.Get(id);
    }

    public void UpdateWarehouse(int id, Warehouse newWarehouse)
    {
        var w = this._warehouseRepository.Get(id);
        w.WarehouseCity = newWarehouse.WarehouseCity;
        w.WarehouseName = newWarehouse.WarehouseName;
        w.WarehouseAddress = newWarehouse.WarehouseAddress;
        w.WarehouseCountry = newWarehouse.WarehouseCountry;

        this._warehouseRepository.Update(w);
    }
}
