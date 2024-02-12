using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repository.Implementation;

public class CartProductsRepository : ICartProductsRepository
{
    private readonly ApplicationDbContext _context;

    public CartProductsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(CartProducts entity)
    {
        this._context.Add<CartProducts>(entity);
        this._context.SaveChanges();
    }

    public void Delete(CartProducts entity)
    {
        this._context.Remove<CartProducts>(entity);
        this._context.SaveChanges();
    }

    public CartProducts Get(int? id)
    {
        return this._context.CartProducts.SingleOrDefault(x => x.Id == id);
    }

    public ICollection<CartProducts> GetAll()
    {
        return this._context.CartProducts
            .Include(x => x.Cart)
            .Include(x => x.CartProduct)
            .ToList();
    }

    public CartProducts GetByCartId(int cartId)
    {
        return this._context.CartProducts.SingleOrDefault(x => x.CartId == cartId);
    }

    public void Update(CartProducts entity)
    {
        this._context.Update<CartProducts>(entity);
        this._context.SaveChanges();
    }

    public void UpdateQuantity(int cartId, int quantity)
    {
        var cp = this.GetByCartId(cartId);
        cp.CartProductQuantity += quantity;

        _context.SaveChanges();
    }
}
