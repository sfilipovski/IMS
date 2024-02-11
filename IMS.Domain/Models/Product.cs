using IMS.Domain.Common;
using IMS.Domain.Relationship;

namespace IMS.Domain.Models;

public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImageUrl { get; set; }
    public string ProdctSKU { get; set; }
    public double ProductPrice { get; set; }
    public int? ProductCategoryId { get; set; }
    public Category? ProductCategory { get; set; }
    public int? ProductSupplierId { get; set; }
    public Supplier? ProductSupplier { get; set; }
    public virtual ICollection<CartProducts>? CartProducts { get; set; }
    public virtual ICollection<OrderProducts>? OrderProducts { get; set; }
    public virtual ICollection<WarehouseProducts>? WarehouseProducts { get; set; }
}
