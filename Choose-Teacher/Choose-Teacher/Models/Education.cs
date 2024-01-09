using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class Education
    {
        public int EducationId { get; set; }
        [Required(ErrorMessage = "Input Majoring")]
        [Display(Name = "Majoring")]
        public string Majoring { get; set; }
        [Required(ErrorMessage = "Input University")]
        [Display(Name = "University")]
        public string University { get; set; }
        [Required(ErrorMessage = "Input Graduation Year")]
        [Display(Name = "Graduation Year")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GraduationYear { get; set; } 

    }
}
