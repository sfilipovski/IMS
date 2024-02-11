using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;
using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using IMS.Service.Interface;

namespace IMS.Service.Implementation;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IRepository<WarehouseProducts> _warehouseProductsRepository;

    public ProductService(IRepository<Product> repository, IRepository<Category> categoryRepository, IRepository<Supplier> supplierRepository, IRepository<WarehouseProducts> warehouseProductsRepository, IRepository<Warehouse> warehouseRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
        _warehouseProductsRepository = warehouseProductsRepository;
        _warehouseRepository = warehouseRepository;
    }

    public void CreateNewProduct(Product product)
    {
        product.ProductCategory = _categoryRepository.Get(product.ProductCategoryId);
        product.ProductSupplier = _supplierRepository.Get(product.ProductSupplierId);
        this._repository.Create(product);
    }

    public void DeleteProduct(int? id)
    {
        var product = this._repository.Get(id); 
        this._repository.Delete(product);
    }

    public List<Product> GetAllProducts()
    {
        return this._repository.GetAll().ToList();
    }

    public Product GetProductById(int? id)
    {
        return _repository.Get(id);
    }

    public void UpdateProduct(int id, Product product)
    {
        var p = _repository.Get(id);
        p.ProductName = product.ProductName;
        p.ProductPrice = product.ProductPrice;
        p.ProdctSKU = product.ProdctSKU;
        p.ProductDescription = product.ProductDescription;
        p.ProductImageUrl = product.ProductImageUrl;
        p.ProductCategoryId = product.ProductCategoryId;
        p.ProductSupplierId = product.ProductSupplierId;
        p.ProductCategory = _categoryRepository.Get(p.ProductCategoryId);
        p.ProductSupplier = _supplierRepository.Get(p.ProductSupplierId);

        this._repository.Update(p);
    }


    public void AddProductToWarehouse(WarehouseProducts wp)
    {
        Product product = this._repository.Get(wp.WarehouseProductId);
        Warehouse warehouse = this._warehouseRepository.Get(wp.WarehouseProductId);

        WarehouseProducts warehouseProducts = new WarehouseProducts
        {
            WarehouseProduct = product,
            WarehouseProductId = wp.WarehouseProductId,
            Warehouse = warehouse,
            WarehouseId = wp.WarehouseId,
            QuantityInStock = wp.QuantityInStock,
            ReorderLimit = wp.ReorderLimit
        };

        this._warehouseProductsRepository.Create(warehouseProducts);
    }

    public WarehouseProducts GetSelectedProduct(int id)
    {
        Product product = this._repository.Get(id);

        WarehouseProducts warehouseProduct = new WarehouseProducts
        {
            WarehouseProduct = product,
            WarehouseProductId = id,
            QuantityInStock = 1,
            ReorderLimit = 1
        };

        return warehouseProduct;
    }
}
