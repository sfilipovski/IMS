using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;

namespace IMS.Service.Interface;

public interface IProductService
{
    List<ProductDto> GetAllProducts();
    ProductDto GetProductById(int? id);
    void CreateNewProduct(CreateProductDto product);
    void UpdateProduct(int id, CreateProductDto product);    
    void DeleteProduct(int? id);
}
