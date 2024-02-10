using IMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Shipment : BaseEntity
{
    public string ShipmentCompanyName { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string ShipmentStatus { get; set; }
    public string ShipmentType { get; set; }
    public string ShipmentAddress { get; set; }
    public string ShipmentCity { get; set; }
    public string ShipmentCountry { get; set; }
    public int ShippingOrderId { get; set; }
    public Order ShippingOrder { get; set; }
}
