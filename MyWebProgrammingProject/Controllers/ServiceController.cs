using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
                .Include(s => s.Gym)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return View(services);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _context.Services
                .Include(s => s.Gym)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null) return NotFound();
            return View(service);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new ServiceFormViewModel
            {
                Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceFormViewModel vm)
        {
            vm.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();

            if (!ModelState.IsValid)
                return View(vm);

            _context.Services.Add(vm.Service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            var vm = new ServiceFormViewModel
            {
                Service = service,
                Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceFormViewModel vm)
        {
            if (id != vm.Service.Id) return BadRequest();

            vm.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();

            if (!ModelState.IsValid)
                return View(vm);

            var db = await _context.Services.FindAsync(id);
            if (db == null) return NotFound();

            db.Name = vm.Service.Name;
            db.Duration = vm.Service.Duration;
            db.Price = vm.Service.Price;
            db.GymId = vm.Service.GymId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services
                .Include(s => s.Gym)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
