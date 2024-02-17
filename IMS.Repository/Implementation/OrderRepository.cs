using IMS.Domain.Models;
using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repository.Implementation;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Order CreateOrder(Order order)
    {
        this._context.Orders.Add(order);
        
        this._context.SaveChanges();
        return order;
    }

    public void DeleteOrder(Order order)
    {
       this._context.Remove(order);
        this._context.SaveChanges();
    }

    public List<Order> GetAllOrders()
    {
        return this._context.Orders.ToList();
    }

    public Order GetByOrderId(int id)
    {
        return this._context.Orders.SingleOrDefault(x => x.Id == id);
    }

    public List<Order> GetOrdersByCustomerId(string id)
    {
        return this._context.Orders
            .AsNoTracking()
            .Include(x => x.OrderProducts)
            .Include(x => x.Shipment)
            .Where(x => x.CustomerId == id)
            .ToList();
    }

    public void UpdateOrder(Order order)
    {
        this._context.Update(order).Property(x => x.Id).IsModified = false;
        this._context.SaveChanges();
    }
}
