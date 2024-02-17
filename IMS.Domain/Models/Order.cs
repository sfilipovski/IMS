using IMS.Domain.Common;
using IMS.Domain.Identity;
using IMS.Domain.Relationship;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Domain.Models;

public class Order : BaseEntity
{
    [DisplayName("Order Date")]
    public DateTime OrderDateCreated { get; set; } = DateTime.Now;
    [DisplayName("Order Status")]
    public string OrderStatus { get; set; } = "Created";
    [Range(0, 1000000)]
    [DisplayName("Total")]
    public double OrderTotalPrice { get; set; }
    [ForeignKey("CustomerId")]
    public string CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Shipment? Shipment { get; set; }
    public virtual ICollection<OrderProducts>? OrderProducts { get; set; }
}
