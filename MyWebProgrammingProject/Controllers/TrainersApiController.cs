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

        // ----------------------------
        // 1️⃣ TÜM EĞİTMENLER
        // GET: /api/trainersapi
        // ----------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainers = await _context.Trainers
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

        // ----------------------------
        // 2️⃣ TARİHE GÖRE MÜSAİT EĞİTMENLER
        // GET: /api/trainersapi/available?date=2025-01-01
        // ----------------------------
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTrainers(DateTime date)
        {
            var trainers = await _context.Trainers
                .Where(t =>
                    !_context.Appointments.Any(a =>
                        a.TrainerId == t.Id &&
                        a.StartTime.Date == date.Date
                    )
                )
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
