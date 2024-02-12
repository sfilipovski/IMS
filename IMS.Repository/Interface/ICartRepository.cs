using IMS.Domain.Models;

namespace IMS.Repository.Interface;

public interface ICartRepository
{
    Cart GetActiveCart(string customerId);
    Cart GetCartById(int id);

}
