using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainerAvailabilityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainerAvailabilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.TrainerAvailabilities
                .Include(a => a.Trainer)
                .OrderByDescending(a => a.StartTime)
                .ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.TrainerAvailabilities
                .Include(a => a.Trainer)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new TrainerAvailabilityFormViewModel
            {
                Trainers = await _context.Trainers.OrderBy(t => t.FullName).ToListAsync(),
                Availability = new TrainerAvailability
                {
                    StartTime = DateTime.Today.AddHours(10),
                    EndTime = DateTime.Today.AddHours(11),
                    Date = DateTime.Today
                }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainerAvailabilityFormViewModel vm)
        {
            vm.Trainers = await _context.Trainers.OrderBy(t => t.FullName).ToListAsync();
            vm.Availability.Date = vm.Availability.StartTime.Date;

            if (!ModelState.IsValid)
                return View(vm);

            if (await HasConflict(vm.Availability))
            {
                ModelState.AddModelError(string.Empty, "Bu eğitmen için seçilen aralıkta başka bir müsaitlik kaydı var.");
                return View(vm);
            }

            _context.TrainerAvailabilities.Add(vm.Availability);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.TrainerAvailabilities.FindAsync(id);
            if (item == null) return NotFound();

            var vm = new TrainerAvailabilityFormViewModel
            {
                Availability = item,
                Trainers = await _context.Trainers.OrderBy(t => t.FullName).ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainerAvailabilityFormViewModel vm)
        {
            if (id != vm.Availability.Id) return BadRequest();

            vm.Trainers = await _context.Trainers.OrderBy(t => t.FullName).ToListAsync();
            vm.Availability.Date = vm.Availability.StartTime.Date;

            if (!ModelState.IsValid)
                return View(vm);

            var db = await _context.TrainerAvailabilities.FirstOrDefaultAsync(x => x.Id == id);
            if (db == null) return NotFound();

            // Çakışma kontrolünü db dışındaki değerlerle yap
            var temp = new TrainerAvailability
            {
                Id = id,
                TrainerId = vm.Availability.TrainerId,
                StartTime = vm.Availability.StartTime,
                EndTime = vm.Availability.EndTime,
                Date = vm.Availability.Date
            };

            if (await HasConflict(temp))
            {
                ModelState.AddModelError(string.Empty, "Bu eğitmen için seçilen aralıkta başka bir müsaitlik kaydı var.");
                return View(vm);
            }

            db.TrainerId = vm.Availability.TrainerId;
            db.StartTime = vm.Availability.StartTime;
            db.EndTime = vm.Availability.EndTime;
            db.Date = vm.Availability.Date;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.TrainerAvailabilities
                .Include(a => a.Trainer)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.TrainerAvailabilities.FindAsync(id);
            if (item != null)
            {
                _context.TrainerAvailabilities.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HasConflict(TrainerAvailability a)
        {
            return await _context.TrainerAvailabilities.AnyAsync(x =>
                x.TrainerId == a.TrainerId &&
                x.Id != a.Id &&
                (
                    (a.StartTime >= x.StartTime && a.StartTime < x.EndTime) ||
                    (a.EndTime > x.StartTime && a.EndTime <= x.EndTime) ||
                    (a.StartTime <= x.StartTime && a.EndTime >= x.EndTime)
                )
            );
        }
    }
}
