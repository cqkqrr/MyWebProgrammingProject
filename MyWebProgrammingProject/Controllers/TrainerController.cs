using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers
                .Include(t => t.Gym)
                .Include(t => t.Services)
                .OrderBy(t => t.FullName)
                .ToListAsync();
            return View(trainers);
        }

        public IActionResult Create()
        {
            ViewBag.Gyms = _context.Gyms.OrderBy(g => g.Name).ToList();
            ViewBag.Services = _context.Services.OrderBy(s => s.Name).ToList();
            return View(new Trainer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer trainer, int[] selectedServiceIds)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Gyms = _context.Gyms.OrderBy(g => g.Name).ToList();
                ViewBag.Services = _context.Services.OrderBy(s => s.Name).ToList();
                return View(trainer);
            }

            if (selectedServiceIds != null && selectedServiceIds.Length > 0)
            {
                trainer.Services = await _context.Services
                    .Where(s => selectedServiceIds.Contains(s.Id))
                    .ToListAsync();
            }

            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Eğitmen başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // Edit, Delete ve Details metotlarını da benzer şekilde sadeleştirip kullanabilirsin.
    }
}