using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;
using MyWebProgrammingProject.Models.ViewModels; // ViewModel ekledik

namespace MyWebProgrammingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // UserManager ve DbContext lazım
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Eğer kullanıcı giriş yapmışsa DASHBOARD göster
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    // Sıradaki randevuyu bul
                    var nextAppt = await _context.Appointments
                        .Include(a => a.Trainer)
                        .Include(a => a.Service)
                        .Where(a => a.UserId == user.Id && a.StartTime > DateTime.Now && a.IsApproved)
                        .OrderBy(a => a.StartTime)
                        .FirstOrDefaultAsync();

                    // Geçmiş antrenman sayısı
                    var totalWorkouts = await _context.Appointments
                        .Where(a => a.UserId == user.Id && a.StartTime < DateTime.Now && a.IsApproved)
                        .CountAsync();

                    var model = new MemberDashboardViewModel
                    {
                        FullName = user.FullName ?? user.UserName,
                        Weight = user.Weight,
                        Height = user.Height,
                        Goal = user.Goal ?? "Hedef Belirlenmedi",
                        NextAppointment = nextAppt,
                        TotalWorkouts = totalWorkouts
                    };

                    // Dashboard view'ını döndür (Aynı Index.cshtml içinde kontrol edeceğiz)
                    return View(model);
                }
            }

            // Giriş yapmamışsa standart Landing Page döner
            return View();
        }

        // ... Diğer metodlar (Privacy, Error) aynı kalsın ...
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}