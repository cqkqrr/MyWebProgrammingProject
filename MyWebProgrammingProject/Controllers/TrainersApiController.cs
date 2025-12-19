using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;

namespace MyWebProgrammingProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Tüm antrenörleri getir
        [HttpGet]
        public async Task<IActionResult> GetAllTrainers()
        {
            var trainers = await _context.Trainers
                .Select(t => new
                {
                    t.Id,
                    t.FullName,
                    t.Expertise
                })
                .ToListAsync();

            return Ok(trainers);
        }

        // 2️⃣ Belirli tarihte müsait antrenörler
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTrainers(DateTime date)
        {
            var trainers = await _context.Trainers
                .Where(t =>
                    _context.TrainerAvailabilities.Any(a =>
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
