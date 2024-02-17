using System.ComponentModel;

namespace IMS.Domain.DTO.Command;

public class CreateShipmentDto
{
    [DisplayName("Company Name")]
    public string ShipmentCompanyName { get; set; }
    [DisplayName("Shipment Type")]
    public string ShipmentType { get; set; }
}
