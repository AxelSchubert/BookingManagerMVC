using BookingManagerMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagerMVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            List<Course> courses = new List<Course>
            {
              new Course { Id = 1, CourseName = "Pad Thai", Price = 120, Description = "Stekta risnudlar med räkor, tofu och jordnötter", IsPopular = true },
              new Course { Id = 2, CourseName = "Grön Curry", Price = 130, Description = "Stark grön curry med kyckling och kokosmjölk", IsPopular = true },
              new Course { Id = 3, CourseName = "Tom Yum Soppa", Price = 120, Description = "Het och sur soppa med räkor och citrongräs", IsPopular = false },
              new Course { Id = 4, CourseName = "Stekt Ris", Price = 110, Description = "Stekt ris med kycling, biff, räkor eller tofu", IsPopular = true }
            };
            return View(courses);
        }
    }
}
