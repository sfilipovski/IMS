using IMS.Domain.Identity;
using IMS.Domain.Models;
using IMS.Domain.Relationship;

namespace IMS.Service.Interface;

public interface ICartService
{
    Cart GetActiveUserCart(string id);
    Cart GetCartById(int id);
    void DeleteCartProduct(int id);
    void DeleteOrder(int id);
    void CreateCart(Cart cart);
    List<CartProducts> GetCartProducts(int cartId);
    CartProducts GetByCartProductsId(int id);
    Order CreateOrder(string customerId);  
    List<Order> GetOrdersByCustomerId(string id);
    List<Shipment> GetShipmentsByCustomerId(string id);
    List<Shipment> GetAllShipments();
    void CreateShipment(Shipment shipment);
    Customer GetCustomer(string customerId);
    Shipment GetShipment(int? shipmentId);
    Order GetOrder(int orderId);
    void UpdateShipment(Shipment shipment);
    void UpdateOrder(Order order);
}
