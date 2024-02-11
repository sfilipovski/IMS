using IMS.Domain.Common;
using IMS.Domain.Models;

namespace IMS.Domain.Relationship;

public class WarehouseProducts : BaseEntity
{
    public Warehouse? Warehouse { get; set; }
    public int WarehouseId { get; set; }
    public Product? WarehouseProduct { get; set; }
    public int WarehouseProductId { get; set; }
    public int QuantityInStock { get; set; }
    public int ReorderLimit { get; set; }
}