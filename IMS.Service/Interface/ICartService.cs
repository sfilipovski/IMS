using IMS.Domain.Models;
using IMS.Domain.Relationship;

namespace IMS.Service.Interface;

public interface ICartService
{
    Cart GetActiveUserCart(string id);
    Cart GetCartById(int id);
    List<CartProducts> GetCartProducts(int cartId);
}
