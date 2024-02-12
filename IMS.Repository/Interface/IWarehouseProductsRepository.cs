using IMS.Domain.Relationship;

namespace IMS.Repository.Interface;

public interface IWarehouseProductsRepository
{
    ICollection<WarehouseProducts> GetAll();
    WarehouseProducts Get(int? id);
    void Create(WarehouseProducts entity);
    void Update(WarehouseProducts entity);
    void Delete(WarehouseProducts entity);
    bool UpdateQuantity(int? warehouseId, int productId, int quantity);
    bool ReorderQuantity(int? warehouseId, int productId, int quantity);
    List<WarehouseProducts> GetByProductId(int productId);
    List<WarehouseProducts> GetByWarehouseId(int warehouseId);
    WarehouseProducts GetByProductIdAndWarehouseId(int productId, int? warehouseId);
}
