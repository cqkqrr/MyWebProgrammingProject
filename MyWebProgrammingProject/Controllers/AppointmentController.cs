using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
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

            var appointments = await query.ToListAsync();
            return View(appointments);
        }

        [Authorize(Roles = "Member")]
        public IActionResult Create()
        {
            ViewBag.Trainers = _context.Trainers.Include(t => t.Gym).ToList();
            ViewBag.Services = _context.Services.Include(s => s.Gym).ToList();
            return View(new Appointment { StartTime = DateTime.Now.AddHours(1) });
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            ViewBag.Trainers = _context.Trainers.Include(t => t.Gym).ToList();
            ViewBag.Services = _context.Services.Include(s => s.Gym).ToList();

            if (appointment.TrainerId <= 0 || appointment.ServiceId <= 0 || appointment.StartTime == default)
            {
                ModelState.AddModelError("", "Eğitmen, hizmet ve başlangıç zamanı zorunludur.");
                return View(appointment);
            }

            var trainer = await _context.Trainers
                .Include(t => t.Services)
                .FirstOrDefaultAsync(t => t.Id == appointment.TrainerId);

            if (trainer == null)
            {
                ModelState.AddModelError("", "Seçilen eğitmen bulunamadı.");
                return View(appointment);
            }

            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == appointment.ServiceId);
            if (service == null)
            {
                ModelState.AddModelError("", "Seçilen hizmet bulunamadı.");
                return View(appointment);
            }

            if (!trainer.Services.Any(s => s.Id == appointment.ServiceId))
            {
                ModelState.AddModelError("", "Seçilen eğitmen bu hizmeti veremiyor.");
                return View(appointment);
            }

            appointment.EndTime = appointment.StartTime.AddMinutes(service.Duration);

            var isWithinAvailability = await _context.TrainerAvailabilities.AnyAsync(a =>
                a.TrainerId == appointment.TrainerId &&
                appointment.StartTime >= a.StartTime &&
                appointment.EndTime <= a.EndTime
            );

            if (!isWithinAvailability)
            {
                ModelState.AddModelError("", "Seçilen saat aralığı, eğitmenin müsaitlik saatleri dışında.");
                return View(appointment);
            }

            var isConflict = await _context.Appointments.AnyAsync(a =>
                a.TrainerId == appointment.TrainerId &&
                (
                    (appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                    (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime) ||
                    (appointment.StartTime <= a.StartTime && appointment.EndTime >= a.EndTime)
                )
            );

            if (isConflict)
            {
                ModelState.AddModelError("", "Seçilen saat aralığında eğitmenin başka bir randevusu var.");
                return View(appointment);
            }

            appointment.IsApproved = false;
            appointment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            appointment.IsApproved = true;
            appointment.AdminMessage = "Randevunuz onaylanmıştır. Görüşmek üzere 💪";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Trainer)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null) return NotFound();
            return View(appointment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string adminMessage)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            appointment.IsApproved = false;
            appointment.AdminMessage = string.IsNullOrWhiteSpace(adminMessage) ? "Randevunuz reddedildi." : adminMessage;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
