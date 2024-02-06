namespace Choose_Teacher.Models.ViewModels
{
    public class BookingViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }
        public double BookHour(TimeSpan StartTime, TimeSpan EndTime)
        {
            TimeSpan duration = EndTime - StartTime;
            return duration.TotalHours;
        }
    }
}
