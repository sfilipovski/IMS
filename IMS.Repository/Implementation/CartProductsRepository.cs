using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;

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
        //this._context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT CartProducts ON");
        this._context.CartProducts.Add(entity);
        this._context.SaveChanges();
        //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT CartProducts OFF");
    }

    public void Delete(CartProducts entity)
    {
        this._context.Remove<CartProducts>(entity);
        this._context.SaveChanges();
    }

    public CartProducts Get(int? id)
    {
        return this._context.CartProducts
            .AsNoTracking()
            .Include(x => x.CartProduct)
            .SingleOrDefault(x => x.Id == id);
    }

    public ICollection<CartProducts> GetAll()
    {
        return this._context.CartProducts
            .Include(x => x.CartProduct)
            .Include(x => x.Cart)
            .AsNoTracking()
            .ToList();
    }

    public CartProducts GetByCartId(int cartId)
    {
        return this._context.CartProducts.FirstOrDefault(x => x.CartId == cartId);
    }

    public CartProducts GetByProductId(int productId)
    {
        return this._context.CartProducts.FirstOrDefault(x => x.CartProductId == productId);
    }

    public CartProducts GetByProductIdAndCartId(int productId, int cartId)
    {
        return this._context.CartProducts.FirstOrDefault(x => x.CartId == cartId && x.CartProductId == productId);
    }

    public List<CartProducts> GetProductsByCartId(int cartId)
    {
        return this._context.CartProducts
            .AsNoTracking()
            .Include(x => x.CartProduct)
            .Where(x => x.CartId == cartId)
            .ToList();
    }

    public void Update(CartProducts entity)
    {
        this._context.Update<CartProducts>(entity).Property(x => x.Id).IsModified = false;
        this._context.SaveChanges();
    }

    public void UpdateQuantity(int cartId, int productId, int quantity)
    {
        var cp = this.GetByProductIdAndCartId(productId, cartId);
        cp.CartProductQuantity += quantity;

        this.Update(cp);
    }
}
