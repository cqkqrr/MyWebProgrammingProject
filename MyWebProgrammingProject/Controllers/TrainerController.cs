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

        [AllowAnonymous] // Herkes görebilir
        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers
                .Include(t => t.Gym)
                .Include(t => t.Services)
                .OrderBy(t => t.FullName)
                .ToListAsync();
            return View(trainers);
        }

        public async Task<IActionResult> Create()
        {
            await LoadViewBags();
            return View(new Trainer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer trainer, int[] selectedServiceIds)
        {
            if (!ModelState.IsValid)
            {
                await LoadViewBags();
                return View(trainer);
            }

            // ✅ SEÇİLEN HİZMETLERİ EKLEME MANTIĞI
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

        // Edit, Delete metodları buraya eklenebilir (önceki kodlarındaki gibi)

        private async Task LoadViewBags()
        {
            ViewBag.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();
            ViewBag.Services = await _context.Services.OrderBy(s => s.Name).ToListAsync();
        }
    }
}