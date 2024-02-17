using IMS.Domain.Models;

namespace IMS.Repository.Interface;

public interface IShipmentRepository
{
    List<Shipment> GetShipments();
    void Create(Shipment shipment);
    void Delete(Shipment shipment);
    List<Shipment> GetCustomerShipments(string customerId);
    Shipment GetShipment(int? id);
    void Update(Shipment shipment);
}
