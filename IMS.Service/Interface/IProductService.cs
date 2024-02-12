using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;
using IMS.Domain.Relationship;

namespace IMS.Service.Interface;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product GetProductById(int? id);
    void CreateNewProduct(Product product);
    void UpdateProduct(int id, Product product);    
    void DeleteProduct(int? id);
    void AddProductToWarehouse(WarehouseProducts wp);
    WarehouseProducts GetSelectedProduct(int id);
    List<Warehouse> GetWarehousesWithProductId(int productId);
    AddCartProductDto GetCartProduct(int id);
    bool AddCartProduct(AddCartProductDto cartProduct, string customerId);
}
