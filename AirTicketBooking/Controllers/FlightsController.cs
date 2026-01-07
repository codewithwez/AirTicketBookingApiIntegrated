using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirTicketBooking.Data;
using AirTicketBooking.Models;

namespace AirTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirTicketDbContext _context;

        public FlightsController(AirTicketDbContext context)
        {
            _context = context;
        }

        // GET: api/flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            return await _context.Flights.ToListAsync();
        }

        // GET: api/flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();
            return flight;
        }

        // POST: api/flights
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            string validationError = GetValidationError(flight);
            if (validationError != null) return BadRequest(validationError);

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFlight), new { id = flight.FlightId }, flight);
        }

        // PUT: api/flights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightId) return BadRequest("ID Mismatch");

            string validationError = GetValidationError(flight);
            if (validationError != null) return BadRequest(validationError);

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Flights.Any(e => e.FlightId == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method for custom validation rules
        private string? GetValidationError(Flight flight)
        {
            if (flight.DepartureTime >= flight.ArrivalTime)
                return "Departure time must be earlier than arrival time.";

            if (flight.FromCity.Equals(flight.ToCity, StringComparison.OrdinalIgnoreCase))
                return "Departure and Arrival cities cannot be the same.";

            return null;
        }
    }
}