using IMS.Domain.Models;
using IMS.Domain.Relationship;

namespace IMS.Repository.Interface;

public interface IOrderRepository
{
    List<Order> GetAllOrders();
    List<Order> GetOrdersByCustomerId(string id);
    Order GetByOrderId(int id);
    Order CreateOrder(Order order);
    void UpdateOrder(Order order);  
    void DeleteOrder(Order order);

}
