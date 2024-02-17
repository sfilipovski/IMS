using IMS.Domain.Common;
using IMS.Domain.Relationship;
using System.ComponentModel;

namespace IMS.Domain.Models;

public class Product : BaseEntity
{
    [DisplayName("Product Name")]
    public string ProductName { get; set; }
    [DisplayName("Product Description")]
    public string? ProductDescription { get; set; }
    [DisplayName("Product Image URL")]
    public string? ProductImageUrl { get; set; }
    [DisplayName("SKU")]
    public string? ProdctSKU { get; set; }
    [DisplayName("Price")]
    public double ProductPrice { get; set; }
    public int? ProductCategoryId { get; set; }
    public Category? ProductCategory { get; set; }
    public int? ProductSupplierId { get; set; }
    public Supplier? ProductSupplier { get; set; }
    public virtual ICollection<CartProducts>? CartProducts { get; set; }
    public virtual ICollection<OrderProducts>? OrderProducts { get; set; }
    public virtual ICollection<WarehouseProducts>? WarehouseProducts { get; set; }
}
