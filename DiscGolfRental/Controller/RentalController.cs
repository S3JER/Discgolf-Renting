using DiscGolfRental.Db;
using DiscGolfRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscGolfRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly DiscDatabaseContext _context;

        public RentalController(DiscDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("create-rental")]
        public async Task<IActionResult> CreateRental(Rental rental)
        {
            if (rental.DueDate > rental.RentalDate.AddDays(3))
            {
                return BadRequest("Lejeperioden kan ikke være længere end 3 dage.");
            }

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRentalById), new { id = rental.Id }, rental);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRentalById(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return rental;
        }
    }
}
