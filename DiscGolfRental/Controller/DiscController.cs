using DiscGolfRental.Db;
using DiscGolfRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscGolfRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscController : ControllerBase
    {
        private readonly DiscDatabaseContext _context;

        public DiscController(DiscDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("available-discs")]
        public async Task<ActionResult<IEnumerable<Disc>>> GetAvailableDiscs()
        {
            var availableDiscs = await _context.Discs
                .Where(d => !_context.Rentals.Any(r => r.DiscId == d.Id && r.DueDate >= DateTime.Now))
                .ToListAsync();

            return Ok(availableDiscs);
        }
    }
}
