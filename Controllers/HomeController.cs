using System.Diagnostics;
using BookingManagerMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Dummydata, ändra senare  
            List<Course> courses = new List<Course>
            {
              new Course { Id = 1, CourseName = "Pad Thai", Price = 120, Description = "Stekta risnudlar med räkor, tofu och jordnötter", IsPopular = true },
              new Course { Id = 2, CourseName = "Grön Curry", Price = 130, Description = "Stark grön curry med kyckling och kokosmjölk", IsPopular = true },
              new Course { Id = 3, CourseName = "Tom Yum Soppa", Price = 120, Description = "Het och sur soppa med räkor och citrongräs", IsPopular = false },
              new Course { Id = 4, CourseName = "Stekt Ris", Price = 110, Description = "Stekt ris med kycling, biff, räkor eller tofu", IsPopular = true }
            };

            var popularCourses = courses.Where(c => c.IsPopular).ToList();

            return View(popularCourses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
