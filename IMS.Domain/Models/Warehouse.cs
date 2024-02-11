using IMS.Domain.Common;
using IMS.Domain.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Warehouse : BaseEntity
{
    public string WarehouseName { get; set; }
    public string? WarehouseAddress { get; set; }
    public string WarehouseCountry { get; set; }
    public string WarehouseCity { get; set; }
    public virtual ICollection<WarehouseProducts>? WarehouseProducts { get; set; }
}
