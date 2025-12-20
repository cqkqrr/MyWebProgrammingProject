using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;

namespace MyWebProgrammingProject.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/trainersapi
        // LINQ: Select + Include join (Gym)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainers = await _context.Trainers
                .Include(t => t.Gym)
                .Select(t => new
                {
                    t.Id,
                    t.FullName,
                    t.Expertise,
                    Gym = t.Gym.Name
                })
                .ToListAsync();

            return Ok(trainers);
        }

        // GET: /api/trainersapi/available?date=2025-01-01
        // Bir gün içinde EN AZ 1 availability kaydı olan eğitmenler
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTrainers(DateTime date)
        {
            var trainers = await _context.Trainers
                .Where(t => _context.TrainerAvailabilities.Any(a =>
                    a.TrainerId == t.Id &&
                    a.Date.Date == date.Date))
                .Select(t => new
                {
                    t.Id,
                    t.FullName,
                    t.Expertise
                })
                .ToListAsync();

            return Ok(trainers);
        }
    }
}
