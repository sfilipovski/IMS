using IMS.Domain.Models;
using IMS.Repository.Interface;

namespace IMS.Repository.Implementation;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Cart GetActiveCart(string customerId)
    {
        return _context.Carts.SingleOrDefault(x => x.CustomerId == customerId);
    }

    public Cart GetCartById(int id)
    {
        return _context.Carts.SingleOrDefault(x => x.Id == id);
    }
}
