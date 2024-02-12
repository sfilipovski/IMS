using IMS.Domain.Models;
using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using IMS.Service.Interface;

namespace IMS.Service.Implementation;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly ICartProductsRepository _cartProductsRepository;

    public CartService(ICartRepository repository, ICartProductsRepository cartProductsRepository)
    {
        _repository = repository;
        _cartProductsRepository = cartProductsRepository;
    }

    public Cart GetActiveUserCart(string id)
    {
        var cart = _repository.GetActiveCart(id);
        if(cart == null)
        {
            return null;
        }

        return cart;
    }

    public Cart GetCartById(int id)
    {
        var cart = _repository.GetCartById(id);
        if (cart == null)
        {
            return null;
        }

        return cart;
    }

    public List<CartProducts> GetCartProducts(int cartId)
    {
        return this._cartProductsRepository.GetAll().Where(x => x.CartId == cartId).ToList();
    }
}
