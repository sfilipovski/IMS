using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.DTO.Query;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductSKU { get; set; }
    public string ProductImageUrl { get; set; }
    public double ProductPrice { get; set; }
    public int? CategoryId { get; set; }
    public int? SupplierId { get; set; }
}
