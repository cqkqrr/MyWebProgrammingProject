using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebProgrammingProject.Data;
using MyWebProgrammingProject.Models.DTOs;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAll()
        {
            var trainers = await _context.Trainers
                .Include(t => t.Gym)
                .Select(t => new TrainerDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Expertise = t.Expertise,
                    GymName = t.Gym.Name
                })
                .ToListAsync();

            return Ok(trainers);
        }
    }
}