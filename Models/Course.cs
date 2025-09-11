namespace BookingManagerMVC.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsPopular { get; set; }
    }
}
