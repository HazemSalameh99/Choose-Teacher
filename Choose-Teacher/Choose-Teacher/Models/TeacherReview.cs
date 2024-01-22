using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class TeacherReview
    {
        public int TeacherReviewId { get; set; }
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        [Display(Name = "Comment")]
        public string? Comment { get; set; }
        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Input User")]
        [Display(Name = "User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "Input Teacher")]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
