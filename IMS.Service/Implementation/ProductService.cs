using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;
using IMS.Repository.Interface;
using IMS.Service.Interface;

namespace IMS.Service.Implementation;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IRepository<Category> _categoryRepository; 
    private readonly IRepository<Supplier> _supplierRepository;

    public ProductService(IRepository<Product> repository, IRepository<Supplier> supplierRepository, IRepository<Category> categoryRepository)
    {
        _repository = repository;
        _supplierRepository = supplierRepository;
        _categoryRepository = categoryRepository;
    }

    public void CreateNewProduct(CreateProductDto product)
    {
        Product p = new Product 
        {
            ProdctSKU = product.ProductSKU, 
            ProductName = product.ProductName , 
            ProductImageUrl = product.ProductImageUrl,
            ProductDescription = product.ProductDescription,
            ProductPrice = product.ProductPrice,
            ProductCategoryId = product.CategoryId,
            ProductSupplierId = product.SupplierId
        };
        this._repository.Create(p);
    }

    public void DeleteProduct(int? id)
    {
        var product = this._repository.Get(id); 
        this._repository.Delete(product);
    }

    public List<ProductDto> GetAllProducts()
    {
        return this._repository.GetAll().Select(product => new ProductDto
        {
            ProductId = product.Id,
            ProductSKU = product.ProdctSKU,
            ProductName = product.ProductName,
            ProductImageUrl = product.ProductImageUrl,
            ProductDescription = product.ProductDescription,
            ProductPrice = product.ProductPrice,
            CategoryId = product.ProductCategoryId,
            SupplierId = product.ProductSupplierId
        }).ToList();
    }

    public ProductDto GetProductById(int? id)
    {
        Product p = this._repository.Get(id);

        if (p == null) return null;

        ProductDto dto = new ProductDto
        {
            ProductId = p.Id,
            ProductName = p.ProductName,
            ProductImageUrl = p.ProductImageUrl,
            ProductDescription = p.ProductDescription,
            ProductPrice = p.ProductPrice,
            CategoryId = p.ProductCategoryId,
            SupplierId = p.ProductSupplierId
        };

        return dto;
    }

    public void UpdateProduct(int id, CreateProductDto product)
    {
        Product p = _repository.Get(id);

        if (p == null) return;

        p.ProductName = product.ProductName;
        p.ProdctSKU = product.ProductSKU;
        p.ProductDescription = product.ProductDescription;
        p.ProductImageUrl = product.ProductImageUrl;
        p.ProductPrice = product.ProductPrice;
        p.ProductCategoryId = product.CategoryId;
        p.ProductSupplierId = product.SupplierId;
        p.ProductCategory = _categoryRepository.Get(product.CategoryId);
        p.ProductSupplier = _supplierRepository.Get(product.SupplierId);

        this._repository.Update(p);
    }
}
