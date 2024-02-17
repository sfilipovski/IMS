using IMS.Domain.Common;
using IMS.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Relationship;

public class WarehouseProducts : BaseEntity
{
    public Warehouse? Warehouse { get; set; }
    public int WarehouseId { get; set; }
    [DisplayName("Product")]
    public Product? WarehouseProduct { get; set; }
    public int WarehouseProductId { get; set; }
    [DisplayName("Quantity In Stock")]
    public int QuantityInStock { get; set; }
    [DisplayName("Reorder Limit")]
    public int ReorderLimit { get; set; }
}