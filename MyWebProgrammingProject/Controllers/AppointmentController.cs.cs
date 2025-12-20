using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

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
        var appointments = await _context.Appointments
            .Include(a => a.Trainer)
            .Include(a => a.Service)
            .ToListAsync();

        return View(appointments);
    }

    public IActionResult Create()
    {
        ViewBag.Trainers = _context.Trainers.ToList();
        ViewBag.Services = _context.Services.ToList();

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        bool isConflict = _context.Appointments.Any(a =>
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

        _context.Add(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
