using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;

namespace IMS.Service.Interface;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product GetProductById(int? id);
    void CreateNewProduct(Product product);
    void UpdateProduct(int id, Product product);    
    void DeleteProduct(int? id);
}
