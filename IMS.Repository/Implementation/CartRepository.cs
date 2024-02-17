using IMS.Domain.Models;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repository.Implementation;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ClearCart(int cartId)
    {
        var cart = this.GetCartById(cartId);

        if (cart.CartProducts.Count == 0) return;

        foreach(var item in cart.CartProducts) {
            _context.CartProducts.Remove(item);
        }
        _context.SaveChanges();
    }

    public void Create(Cart cart)
    {
        this._context.Carts.Add(cart);
        this._context.SaveChanges();
    }

    public Cart GetActiveCart(string customerId)
    {
        return _context.Carts
            .AsNoTracking()
            .SingleOrDefault(x => x.CustomerId == customerId);
    }

    public Cart GetCartById(int id)
    {
        return _context.Carts
            .AsNoTracking()
            .Include(x => x.CartProducts)
            .SingleOrDefault(x => x.Id == id);
    }
}
