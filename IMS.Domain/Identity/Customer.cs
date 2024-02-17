using IMS.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Identity;

public class Customer : Account
{
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; }
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    [MaxLength(255)]
    public string Address { get; set; }
    [MaxLength(100)]
    public string Country { get; set; }
    [MaxLength(100)]
    public string City { get; set; }
    public Cart CustomerCart { get; set; } = new Cart();
    //public virtual ICollection<Order>? CustomerOrders { get; set; }
}
