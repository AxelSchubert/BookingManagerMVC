using BookingManagerMVC.Models;
using BookingManagerMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagerMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<IActionResult> Index()
        {
            var courses =  await _menuService.GetMenuAsync();
            return View(courses);
        }
    }
}
