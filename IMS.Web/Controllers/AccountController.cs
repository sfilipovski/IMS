using IMS.Domain.DTO.Command;
using IMS.Domain.Identity;
using IMS.Domain.Models;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace IMS.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly ICartService _cartService;

    public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, ICartService cartService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _cartService = cartService;
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegistrationDto register)
    {

        if (ModelState.IsValid)
        {
            var emailExists = await _userManager.FindByEmailAsync(register.Email);
            if (emailExists != null) return View(ModelState);

            var customer = new Customer
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Address = register.Address,
                City = register.City,
                Country = register.Country,
                UserName = register.Username,
                Email = register.Email,
                CustomerCart = new Domain.Models.Cart()
            };

            var result = await _userManager.CreateAsync(customer, register.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View(ModelState);
        }

        return BadRequest(ModelState);
    }

    public IActionResult Login()
    {
        UserLoginDto dto = new UserLoginDto();
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginDto login)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null) { return View(login); };
            
            var pw = await _userManager.CheckPasswordAsync(user, login.Password);

            if (pw == false) return View(login);

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);

            bool hasClaim = _userManager.GetClaimsAsync(user).Result.Any(x => x.Type.Equals("UserRole") && x.Value == "Customer");

            if (result.Succeeded)
            {
                if (!hasClaim)
                {
                    
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Customer"));
                   
                }
                return RedirectToAction("Index", "Products");
            }
            return View(login);
        }
        
        return View(login);
        
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Cart(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId == null) RedirectToAction("Login");

        var cart = this._cartService.GetActiveUserCart(userId);

        ViewBag.CartId = cart.Id;

        return View(this._cartService.GetCartProducts(cart.Id));
    }

    public IActionResult Order()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var order = this._cartService.CreateOrder(userId);

        if (order != null)
        {
            var shipments = this._cartService.GetAllShipments();
            ViewBag.Shipments = new SelectList(shipments, "Id", "ShipmentCompanyName");
            var orderDto = new AddOrderShipmentDto
            {
                OrderId = order.Id
            };
            return View(orderDto);
        }
        return BadRequest(order);
    
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Order([Bind("OrderId,ShipmentId")] AddOrderShipmentDto model)
    {
        if (ModelState.IsValid)
        {
            var shipment = this._cartService.GetShipment(model.ShipmentId);
            if (shipment == null) { return NotFound(shipment); }
            var order = this._cartService.GetOrder(model.OrderId);
            if(order == null) { return NotFound(order); }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = this._cartService.GetCustomer(userId);

            shipment.ShippingOrderId = order.Id;
            shipment.ShippingOrder = order;
            shipment.ShipmentAddress = customer.Address;
            shipment.ShipmentDate = DateTime.Now;
            shipment.ShipmentCity = customer.City;
            shipment.ShipmentCountry = customer.Country;
            shipment.ShipmentStatus = "In progress";
            this._cartService.UpdateShipment(shipment);
            order.Shipment = shipment;
            this._cartService.UpdateOrder(order);

            return View("GetCustomerOrders");
        }
        return View(ModelState);
    }

    public IActionResult GetCustomerOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) RedirectToAction("Login");

        var orders = this._cartService.GetOrdersByCustomerId(userId);

        if (orders == null) return NotFound(orders);

        return View(orders);
    }

   /* [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Order(string id)
    {
        var createOrder = this._cartService.CreateOrder(id);

        if (createOrder == true) return RedirectToAction("Index", "Products");
    }*/


    public IActionResult Delete(int id)
    {
        var cartProduct = this._cartService.GetByCartProductsId(id);

        return View(cartProduct);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        this._cartService.DeleteCartProduct(id);
        return RedirectToAction("Cart");
    }

    public IActionResult DeleteOrder(int id)
    {
        var order = this._cartService.GetOrder(id);

        return View(order);
    }

    [HttpPost, ActionName("DeleteOrder")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteOrderConfirmed(int id)
    {
        this._cartService.DeleteOrder(id);
        return RedirectToAction("GetCustomerOrders");
    }
}
