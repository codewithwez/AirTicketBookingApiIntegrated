using System.ComponentModel.DataAnnotations;

namespace AirTicketBooking.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [StringLength(20)]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Airline { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FromCity { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ToCity { get; set; } = string.Empty;

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal TicketPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available seats cannot be negative")]
        public int AvailableSeats { get; set; }
    }
}