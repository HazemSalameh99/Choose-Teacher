using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class TeacherAvailability
    {
        public int TeacherAvailabilityId { get; set; }
        [Required(ErrorMessage ="Input Day Of Week")]
        [Display(Name ="Day Of Week")]
        public DayOfWeek dayOfWeek { get; set; }
        [Required(ErrorMessage = "Input Start Time")]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "Input End Time")]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Input Teacher")]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
