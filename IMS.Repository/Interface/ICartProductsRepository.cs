using IMS.Domain.Relationship;

namespace IMS.Repository.Interface;

public interface ICartProductsRepository
{
    ICollection<CartProducts> GetAll();
    CartProducts Get(int? id);
    void Create(CartProducts entity);
    void Update(CartProducts entity);
    void Delete(CartProducts entity);
    CartProducts GetByCartId(int cartId);
    void UpdateQuantity(int cartId, int quantity);
}
