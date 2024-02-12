using IMS.Domain.DTO.Command;
using IMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Customer"));
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
    }
}
