using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Choose_Teacher.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        [Required(ErrorMessage ="Input Teacher Name")]
        [Display(Name ="Teacher")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Input Teacher Email")]
        [Display(Name = "Teacher Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Input Teacher Phone Number")]
        [Display(Name = "Teacher Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int phoneNumber { get; set; }
        [Required(ErrorMessage = "Choose City")]
        [Display(Name = "City")]
        public City City { get; set; }

        [Required(ErrorMessage = "Input Loacation")]
        [Display(Name = "Loacation")]
        public string? Loacation { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }

    }
}
