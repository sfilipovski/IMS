using IMS.Domain.Relationship;
using IMS.Repository.Interface;

namespace IMS.Repository.Implementation;

public class OrderProductsRepository : IRepository<OrderProducts>
{
    private readonly ApplicationDbContext _context;

    public OrderProductsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(OrderProducts entity)
    {
        this._context.OrderProducts.Add(entity);
        this._context.SaveChanges();
    }

    public void Delete(OrderProducts entity)
    {
        this._context.OrderProducts.Remove(entity);
        this._context.SaveChanges();
    }

    public OrderProducts Get(int? id)
    {
        return this._context.OrderProducts.SingleOrDefault(x => x.Id == id);
    }

    public ICollection<OrderProducts> GetAll()
    {
        return this._context.OrderProducts.ToList();
    }

    public void Update(OrderProducts entity)
    {
        this._context.OrderProducts.Update(entity);
        this._context.SaveChanges();
    }
}
