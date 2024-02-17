using IMS.Domain.Identity;
using IMS.Domain.Models;
using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using IMS.Service.Interface;

namespace IMS.Service.Implementation;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly ICartProductsRepository _cartProductsRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IRepository<OrderProducts> _orderProducts;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IAccountRepository _accountRepository;

    public CartService(ICartRepository repository, ICartProductsRepository cartProductsRepository, IOrderRepository orderRepository, IRepository<OrderProducts> orderProducts, IShipmentRepository shipmentRepository, IAccountRepository accountRepository)
    {
        _repository = repository;
        _cartProductsRepository = cartProductsRepository;
        _orderRepository = orderRepository;
        _orderProducts = orderProducts;
        _shipmentRepository = shipmentRepository;
        _accountRepository = accountRepository;
    }

    public void DeleteCartProduct(int id)
    {
        var cp = this._cartProductsRepository.Get(id);
        this._cartProductsRepository.Delete(cp);
    }

    public void DeleteOrder(int id)
    {
        var order = this._orderRepository.GetByOrderId(id);
        this._orderRepository.DeleteOrder(order);
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

    public CartProducts GetByCartProductsId(int id)
    {
        return this._cartProductsRepository.Get(id);
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
        return this._cartProductsRepository
            .GetAll()
            .Where(x => x.CartId == cartId).ToList();
    }

    public Order CreateOrder(string customerId)
    {
        var cart = this.GetActiveUserCart(customerId);

        if (cart == null) return null;

        Order order = new Order
        {
            OrderDateCreated = DateTime.Now,
            OrderStatus = "Created",
            CustomerId = customerId,
            OrderTotalPrice = 1
        };

        var products = this._cartProductsRepository.GetProductsByCartId(cart.Id);

        Order createdOrder = this._orderRepository.CreateOrder(order);

        if (createdOrder == null) return null;

        double total = 0;

        //List<OrderProducts> orderProducts = new List<OrderProducts>();

        foreach(var item in products)
        {
            var product = new OrderProducts
            {
                OrderId = createdOrder.Id,
                OrderProductId = (int)item.CartProductId,
                OrderProductQuantity = item.CartProductQuantity
            };
            total += item.CartProduct.ProductPrice * item.CartProductQuantity;
            this._orderProducts.Create(product);
            //orderProducts.Add(product); 
        }

        createdOrder.OrderTotalPrice = total;
        this._orderRepository.UpdateOrder(createdOrder);

        this._repository.ClearCart(cart.Id);

        return createdOrder;
    }

    public List<Order> GetOrdersByCustomerId(string id)
    {
        return this._orderRepository.GetOrdersByCustomerId(id);
    }

    public List<Shipment> GetShipmentsByCustomerId(string id)
    {
        return this._shipmentRepository.GetCustomerShipments(id);
    }

    public List<Shipment> GetAllShipments()
    {
        return this._shipmentRepository.GetShipments();
    }

    public Customer GetCustomer(string customerId)
    {
        return this._accountRepository.GetCustomerById(customerId);
    }

    public void CreateShipment(Shipment shipment)
    {
        this._shipmentRepository.Create(shipment);
    }

    public Shipment GetShipment(int? id)
    {
        return this._shipmentRepository.GetShipment(id);
    }

    public Order GetOrder(int orderId)
    {
        return this._orderRepository.GetByOrderId(orderId);
    }

    public void UpdateShipment(Shipment shipment)
    {
        this._shipmentRepository.Update(shipment);
    }

    public void UpdateOrder(Order order)
    {
        this._orderRepository.UpdateOrder(order);
    }

    public void CreateCart(Cart cart)
    {
        this._repository.Create(cart);
    }
}
