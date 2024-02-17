using IMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Supplier : BaseEntity
{
    [DisplayName("Supplier")]
    public string SupplierName { get; set; }
    [DisplayName("Supplier Address")]
    public string? SupplierAddress { get; set; }
    [DisplayName("Phone Number")]
    public string? SupplierPhone { get; set;}
}
