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
        private readonly IWebHostEnvironment _env;

        public TrainerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ Tanı ekranı: Dosya nerede aranıyor? Var mı?
        [AllowAnonymous]
        public IActionResult Diagnose()
        {
            var fullPath = Path.Combine(_env.ContentRootPath, "Views", "Trainer", "Index.cshtml");
            var exists = System.IO.File.Exists(fullPath);

            return Content(
                $"ContentRootPath: {_env.ContentRootPath}\n" +
                $"Looking for: {fullPath}\n" +
                $"Exists: {exists}\n\n" +
                $"Eğer Exists: False ise, Index.cshtml dosyan doğru klasörde değil demektir.\n" +
                $"Bu dosyayı yukarıdaki full path'e gelecek şekilde yerleştir."
            );
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers
                .Include(t => t.Gym)
                .Include(t => t.Services)
                .OrderBy(t => t.FullName)
                .ToListAsync();

            return View("~/Views/Trainer/Index.cshtml", trainers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var trainer = await _context.Trainers
                .Include(t => t.Gym)
                .Include(t => t.Services)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null) return NotFound();
            return View("~/Views/Trainer/Details.cshtml", trainer);
        }

        public IActionResult Create()
        {
            ViewBag.Gyms = _context.Gyms.OrderBy(g => g.Name).ToList();
            ViewBag.Services = _context.Services.OrderBy(s => s.Name).ToList();
            return View("~/Views/Trainer/Create.cshtml", new Trainer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer trainer, int[] selectedServiceIds)
        {
            ViewBag.Gyms = _context.Gyms.OrderBy(g => g.Name).ToList();
            ViewBag.Services = _context.Services.OrderBy(s => s.Name).ToList();

            if (!ModelState.IsValid)
                return View("~/Views/Trainer/Create.cshtml", trainer);

            if (selectedServiceIds != null && selectedServiceIds.Length > 0)
            {
                var services = await _context.Services
                    .Where(s => selectedServiceIds.Contains(s.Id))
                    .ToListAsync();

                trainer.Services = services;
            }

            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trainer = await _context.Trainers
                .Include(t => t.Services)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null) return NotFound();

            ViewBag.Gyms = _context.Gyms.OrderBy(g => g.Name).ToList();
            ViewBag.Services = _context.Services.OrderBy(s => s.Name).ToList();
            ViewBag.SelectedServiceIds = trainer.Services.Select(s => s.Id).ToList();

            return View("~/Views/Trainer/Edit.cshtml", trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainer trainer, int[] selectedServiceIds)
        {
            if (id != trainer.Id) return BadRequest();

            ViewBag.Gyms = _context.Gyms.OrderBy(g => g.Name).ToList();
            ViewBag.Services = _context.Services.OrderBy(s => s.Name).ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.SelectedServiceIds = selectedServiceIds?.ToList() ?? new List<int>();
                return View("~/Views/Trainer/Edit.cshtml", trainer);
            }

            var dbTrainer = await _context.Trainers
                .Include(t => t.Services)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (dbTrainer == null) return NotFound();

            dbTrainer.FullName = trainer.FullName;
            dbTrainer.Expertise = trainer.Expertise;
            dbTrainer.GymId = trainer.GymId;

            dbTrainer.Services.Clear();
            if (selectedServiceIds != null && selectedServiceIds.Length > 0)
            {
                var services = await _context.Services
                    .Where(s => selectedServiceIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var s in services)
                    dbTrainer.Services.Add(s);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers
                .Include(t => t.Gym)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trainer == null) return NotFound();
            return View("~/Views/Trainer/Delete.cshtml", trainer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
