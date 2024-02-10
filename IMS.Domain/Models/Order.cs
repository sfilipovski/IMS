using IMS.Domain.Common;
using IMS.Domain.Identity;
using IMS.Domain.Relationship;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Models;

public class Order : BaseEntity
{
    
    public DateTime OrderDateCreated { get; set; } = DateTime.Now;
    public string OrderStatus { get; set; } = "Created";
    [Range(0, 1000000)]
    public double OrderTotalPrice { get; set; }
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Shipment? Shipment { get; set; }
    public virtual ICollection<OrderProducts> OrderProducts { get; set; }
}
