using BookingManagerMVC.Models;
using BookingManagerMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagerMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Tables()
        {
            var tables = await _adminService.GetAsync<Table>("Table");
            return View(tables); 
        }

        public async Task<IActionResult> Bookings()
        {
            var bookings = await _adminService.GetAsync<Booking>("Booking");
            return View(bookings);
        }

        public async Task<IActionResult> Menu()
        {
            var courses = await _adminService.GetAsync<Course>("Course");
            return View(courses);
        }
        [HttpPost]

        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _adminService.DeleteAsync("Course", id);
            if (success)
            {
                return RedirectToAction("Menu");
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> EditCourse(int id, Course updatedCourse)
        {
            var success = await _adminService.EditAsync("Course", id, updatedCourse);
            if (success)
            {
                return RedirectToAction("Menu");
            }
            return BadRequest();

        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course newCourse)
        {
            var success = await _adminService.CreateAsync("Course", newCourse);
            if (success)
            {
                return RedirectToAction("Menu");
            }
            return BadRequest();

        }
        [HttpPost]

        public async Task<IActionResult> DeleteTable(int id)
        {
            var success = await _adminService.DeleteAsync("Table", id);
            if (success)
            {
                return RedirectToAction("Tables");
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> EditTable(int id, Table updatedTable)
        {
            var success = await _adminService.EditAsync("Table", id, updatedTable);
            if (success)
            {
                return RedirectToAction("Tables");
            }
            return BadRequest();

        }
        [HttpPost]
        public async Task<IActionResult> CreateTable(Table newTable)
        {
            var success = await _adminService.CreateAsync("Table", newTable);
            if (success)
            {
                return RedirectToAction("Tables");
            }
            return BadRequest();
        }
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var success = await _adminService.DeleteAsync("Booking", id);
            if (success)
            {
                return RedirectToAction("Bookings");
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> EditBooking(int id, Booking updatedBooking)
        {
            var success = await _adminService.EditAsync("Booking", id, updatedBooking);
            if (success)
            {
                return RedirectToAction("Bookings");
            }
            return BadRequest();

        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking newBooking)
        {
            var success = await _adminService.CreateAsync("Booking", newBooking);
            if (success)
            {
                return RedirectToAction("Bookings");
            }
            return BadRequest();
        }
}
