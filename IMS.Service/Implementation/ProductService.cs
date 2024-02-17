using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Identity;
using IMS.Domain.Models;
using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using IMS.Service.Interface;
using System.Reflection.Metadata.Ecma335;

namespace IMS.Service.Implementation;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IRepository<Warehouse> _warehouseRepository;
    private readonly IWarehouseProductsRepository _warehouseProductsRepository;
    private readonly ICartProductsRepository _cartProductsRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICartRepository _cartRepository;

    public ProductService(IRepository<Product> repository, IRepository<Category> categoryRepository, IRepository<Supplier> supplierRepository, IWarehouseProductsRepository warehouseProductsRepository, IRepository<Warehouse> warehouseRepository, IAccountRepository accountRepository, ICartRepository cartRepository, ICartProductsRepository cartProductsRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
        _warehouseRepository = warehouseRepository;
        _accountRepository = accountRepository;
        _cartRepository = cartRepository;
        _warehouseProductsRepository = warehouseProductsRepository;
        _cartProductsRepository = cartProductsRepository;
    }

    public void CreateNewProduct(Product product)
    {
        /*product.ProductCategory = _categoryRepository.Get(product.ProductCategoryId);
        product.ProductSupplier = _supplierRepository.Get(product.ProductSupplierId);*/
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
        Warehouse warehouse = this._warehouseRepository.Get(wp.WarehouseId);

        if (product == null || warehouse == null) return;

        var wpExists = this._warehouseProductsRepository.GetByProductIdAndWarehouseId(product.Id, warehouse.Id);

        if (wpExists != null)
        {
            this._warehouseProductsRepository.ReorderQuantity(warehouse.Id, product.Id, wp.QuantityInStock);
            return;
        }

        WarehouseProducts warehouseProducts = new WarehouseProducts
        {
            WarehouseProductId = wp.WarehouseProductId,
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

    public AddCartProductDto GetCartProduct(int id)
    {
        Product product = this._repository.Get(id);

        AddCartProductDto cartProduct = new AddCartProductDto
        {
            CartProduct = product,
            ProductId = id,
            WarehouseId = null,
            Quantity = 1
        };

        return cartProduct;
    }

    public bool AddCartProduct(AddCartProductDto request, string customerId)
    {
        var customer = _accountRepository.GetCustomerById(customerId);

        var cart = _cartRepository.GetActiveCart(customerId);

        if (customer == null || cart == null) return false;

        var warehouse = this._warehouseRepository.Get(request.WarehouseId);

        if (warehouse == null) return false;

        var cp = _cartProductsRepository.GetByProductIdAndCartId(request.ProductId, cart.Id);

        if(cp!= null)
        {
            bool update = this._warehouseProductsRepository.UpdateQuantity(request.WarehouseId, request.ProductId, request.Quantity);
            if (update)
            {
                this._cartProductsRepository.UpdateQuantity(cart.Id, request.ProductId, request.Quantity);
                return true;
            }
            return false;
        }


        Product product = this._repository.Get(request.ProductId);
        var cartt = this._cartRepository.GetCartById(cart.Id);

        CartProducts cartProduct = new CartProducts
        {
            CartProductId = request.ProductId,
            CartId = cartt.Id,
            CartProductQuantity = request.Quantity
        };
        //

        if(this._warehouseProductsRepository.UpdateQuantity(request.WarehouseId, request.ProductId, request.Quantity))
        {
            this._cartProductsRepository.Create(cartProduct);
            return true;
        }

        return false;
    }

    public List<Warehouse> GetWarehousesWithProductId(int productId)
    {
        Product product = this._repository.Get(productId);

        if (product == null) return null;

        var warehouses = this._warehouseProductsRepository.GetByProductId(productId)
                                                          .Select(x => this._warehouseRepository.Get(x.WarehouseId))
                                                          .ToList();

        return warehouses;
    }
}
