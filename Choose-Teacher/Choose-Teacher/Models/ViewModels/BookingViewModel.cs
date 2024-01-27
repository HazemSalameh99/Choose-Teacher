namespace Choose_Teacher.Models.ViewModels
{
    public class BookingViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal BookingHour { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }

    }
}
