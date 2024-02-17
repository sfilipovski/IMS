using IMS.Domain.DTO.Command;
using IMS.Domain.Models;
using IMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMS.Web.Controllers;

public class ShipmentsController : Controller
{

    private readonly ICartService _cartService;

    public ShipmentsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        return View(this._cartService.GetAllShipments());
    }

    public IActionResult CustomerShipments()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return RedirectToAction("Login", "Account");

        return View(this._cartService.GetShipmentsByCustomerId(userId));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateShipmentDto shipment)
    {
        if(ModelState.IsValid)
        {
                Shipment createShipment = new Shipment
                {
                    ShipmentCompanyName = shipment.ShipmentCompanyName,
                    ShipmentType = shipment.ShipmentType,
                    ShipmentAddress = "",
                    ShipmentCity = "",
                    ShipmentCountry = "",
                    ShipmentStatus = "",
                    ShipmentDate = DateTime.Now,
                    ShippingOrderId = 13
                };
                this._cartService.CreateShipment(createShipment);
                return RedirectToAction("Index");
           
        }

        return View(ModelState);
    }
}
