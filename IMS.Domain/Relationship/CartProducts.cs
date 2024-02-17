using IMS.Domain.Common;
using IMS.Domain.Identity;
using IMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Relationship;

public class CartProducts : BaseEntity
{
    public Cart? Cart { get; set; }
    public int CartId { get; set; }
    [DisplayName("Product")]
    public Product? CartProduct { get; set; }
    public int CartProductId { get; set; }
    [DisplayName("Quantity")]
    public int CartProductQuantity { get; set; }
}
