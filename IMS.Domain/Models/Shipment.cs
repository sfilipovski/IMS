using IMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Shipment : BaseEntity
{
    [DisplayName("Company Name")]
    public string ShipmentCompanyName { get; set; }
    [DisplayName("Shipment Date")]
    public DateTime? ShipmentDate { get; set; }
    [DisplayName("Shipment Status")]
    public string? ShipmentStatus { get; set; }
    [DisplayName("Shipment Type")]
    public string ShipmentType { get; set; }
    [DisplayName("Shipping Address")]
    public string? ShipmentAddress { get; set; }
    [DisplayName("Shipping City")]
    public string? ShipmentCity { get; set; }
    [DisplayName("Shipping Country")]
    public string? ShipmentCountry { get; set; }
    public int? ShippingOrderId { get; set; }
    public Order? ShippingOrder { get; set; }
}
