using Microsoft.EntityFrameworkCore;
using AirTicketBooking.Models;

namespace AirTicketBooking.Data
{
    public class AirTicketDbContext : DbContext
    {
        public AirTicketDbContext(DbContextOptions<AirTicketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
    }
}