using IMS.Domain.Models;

namespace IMS.Repository.Interface;

public interface ICartRepository
{
    Cart GetActiveCart(string customerId);
    Cart GetCartById(int id);
    void Create(Cart cart);
    void ClearCart(int cartId);
}
