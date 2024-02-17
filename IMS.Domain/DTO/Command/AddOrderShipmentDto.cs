namespace IMS.Domain.DTO.Command;

public class AddOrderShipmentDto
{
    public int OrderId { get; set; }
    public int? ShipmentId { get; set; }
}
