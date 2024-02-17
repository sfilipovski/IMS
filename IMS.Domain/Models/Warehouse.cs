using IMS.Domain.Common;
using IMS.Domain.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Warehouse : BaseEntity
{
    [DisplayName("Warehouse")]
    public string WarehouseName { get; set; }
    [DisplayName("Address")]
    public string? WarehouseAddress { get; set; }
    [DisplayName("Country")]
    public string WarehouseCountry { get; set; }
    [DisplayName("City")]
    public string WarehouseCity { get; set; }
    public virtual ICollection<WarehouseProducts>? WarehouseProducts { get; set; }
}
