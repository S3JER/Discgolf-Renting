namespace DiscGolfRental.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DiscId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }

        public User User { get; set; }
        public Disc Disc { get; set; }
    }
}
