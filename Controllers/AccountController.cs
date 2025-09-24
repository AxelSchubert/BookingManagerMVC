using BookingManagerMVC.Models.ViewModels;
using BookingManagerMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        public AccountController(AuthService authService)
        {
            _authService = authService; 
        }
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Logout");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Både användarnamn och lösenord måste fyllas i";
                return View(viewModel);
            }

            var loginSuccessful = await _authService.Login(viewModel);
            if (loginSuccessful == false)
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ViewBag.Error = "Användarnamn eller lösenord är felaktigt";
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}
