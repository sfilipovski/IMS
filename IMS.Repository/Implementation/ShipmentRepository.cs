using IMS.Domain.Models;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repository.Implementation;

public class ShipmentRepository : IShipmentRepository
{

    private readonly ApplicationDbContext _context;

    public ShipmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Shipment shipment)
    {
        this._context.Shipments.Add(shipment);
        this._context.SaveChanges();
    }

    public void Delete(Shipment shipment)
    {
        this._context.Shipments.Remove(shipment);
        this._context.SaveChanges();
    }

    public List<Shipment> GetShipments()
    {
        return this._context.Shipments
            .AsNoTracking()
            .Include(x => x.ShippingOrder)
            .ToList();
    }

    public List<Shipment> GetCustomerShipments(string customerId)
    {
        return this._context.Shipments
             .AsNoTracking()
             .Include(x => x.ShippingOrder)
             .Where(x => x.ShippingOrder.CustomerId == customerId)
             .ToList();
    }

    public Shipment GetShipment(int? id)
    {
        return this._context.Shipments.SingleOrDefault(x => x.Id == id);
    }

    public void Update(Shipment shipment)
    {
        this._context.Update(shipment).Property(x => x.Id).IsModified = false;
        this._context.SaveChanges();
    }
}
