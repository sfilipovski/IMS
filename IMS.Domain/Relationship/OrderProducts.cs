using IMS.Domain.Common;
using IMS.Domain.Models;

namespace IMS.Domain.Relationship;

public class OrderProducts : BaseEntity
{
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public Product OrderProduct { get; set; }
    public int OrderProductId { get; set; }
    public int OrderProductQuantity { get; set; }
}
