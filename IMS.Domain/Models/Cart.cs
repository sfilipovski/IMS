using IMS.Domain.Common;
using IMS.Domain.Identity;
using IMS.Domain.Relationship;

namespace IMS.Domain.Models;

public class Cart : BaseEntity
{
    public string? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public virtual ICollection<CartProducts>? CartProducts { get; set; }
}
