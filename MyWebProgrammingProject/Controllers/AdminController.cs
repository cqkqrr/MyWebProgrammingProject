using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // /Admin -> /Admin/Dashboard
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                TotalMembers = _context.Users.Count(),
                TotalTrainers = _context.Trainers.Count(),
                TotalAppointments = _context.Appointments.Count(),
                PendingAppointments = _context.Appointments.Count(a => !a.IsApproved)
            };

            return View(model);
        }
    }
}
