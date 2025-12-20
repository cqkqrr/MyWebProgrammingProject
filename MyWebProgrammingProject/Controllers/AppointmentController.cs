using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;
using MyWebProgrammingProject.Services;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentService _appointmentService; // ✅ Servis eklendi

        public AppointmentController(ApplicationDbContext context, IAppointmentService appointmentService)
        {
            _context = context;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var query = _context.Appointments
                .Include(a => a.Trainer).ThenInclude(t => t.Gym)
                .Include(a => a.Service)
                .Include(a => a.User)
                .OrderByDescending(a => a.StartTime)
                .AsQueryable();

            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                query = query.Where(a => a.UserId == userId);
            }

            return View(await query.ToListAsync());
        }

        [Authorize(Roles = "Member")]
        public IActionResult Create()
        {
            LoadViewBags();
            return View(new Appointment { StartTime = DateTime.Now.AddHours(1) });
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (appointment.TrainerId <= 0 || appointment.ServiceId <= 0)
            {
                ModelState.AddModelError("", "Lütfen eğitmen ve hizmet seçiniz.");
            }

            if (!ModelState.IsValid)
            {
                LoadViewBags();
                return View(appointment);
            }

            // Hizmet süresini bul ve bitişi hesapla
            var service = await _context.Services.FindAsync(appointment.ServiceId);
            if (service != null)
            {
                appointment.EndTime = appointment.StartTime.AddMinutes(service.Duration);
            }

            // ✅ 1. Servis ile Müsaitlik Kontrolü
            if (!await _appointmentService.IsTrainerAvailableAsync(appointment.TrainerId, appointment.StartTime, appointment.EndTime))
            {
                ModelState.AddModelError("", "Seçilen saat aralığı, eğitmenin çalışma saatleri dışında.");
                LoadViewBags();
                return View(appointment);
            }

            // ✅ 2. Servis ile Çakışma Kontrolü
            if (await _appointmentService.CheckConflictAsync(appointment.TrainerId, appointment.StartTime, appointment.EndTime))
            {
                ModelState.AddModelError("", "Seçilen saatte eğitmen dolu.");
                LoadViewBags();
                return View(appointment);
            }

            appointment.IsApproved = false;
            appointment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Randevu talebiniz başarıyla alındı!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);
            if (appt == null) return NotFound();

            appt.IsApproved = true;
            appt.AdminMessage = "Randevunuz onaylanmıştır. 💪";
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Randevu onaylandı.";
            return RedirectToAction(nameof(Index));
        }

        // Yardımcı Metot
        private void LoadViewBags()
        {
            ViewBag.Trainers = _context.Trainers.Include(t => t.Gym).ToList();
            ViewBag.Services = _context.Services.Include(s => s.Gym).ToList();
        }

        // Reject metotlarını da buraya ekleyebilirsin, logic aynı kalır.
    }
}