using Choose_Teacher.Models.SharedProp;

namespace Choose_Teacher.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Status Status { get; set; }
      

    }
    public enum Status
    {
        Pending,
        Approved,
        Rejected,
        Canceled
    }
}
