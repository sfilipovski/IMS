using IMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Supplier : BaseEntity
{
    public string SupplierName { get; set; }
    public string SupplierAddress { get; set; }
    public string SupplierPhone { get; set;}
    public virtual ICollection<Product> SupplierProducts { get; set; }
}
