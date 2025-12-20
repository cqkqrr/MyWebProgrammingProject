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

        public async Task<IActionResult> Create()
        {
            // Dropdown verisini ViewBag ile taşıyoruz
            ViewBag.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();
            return View(new Service());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            // ModelState kontrolü
            if (!ModelState.IsValid)
            {
                // Hata varsa dropdown'ı tekrar doldur
                ViewBag.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();
                return View(service);
            }

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Hizmet başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            ViewBag.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Gyms = await _context.Gyms.OrderBy(g => g.Name).ToListAsync();
                return View(service);
            }

            _context.Update(service);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Hizmet güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services.Include(s => s.Gym).FirstOrDefaultAsync(s => s.Id == id);
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
            TempData["SuccessMessage"] = "Hizmet silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}