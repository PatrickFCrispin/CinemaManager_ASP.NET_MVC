namespace CinemaManager.Models
{
    public class TheatersModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        
        public int NumberOfSeats { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}