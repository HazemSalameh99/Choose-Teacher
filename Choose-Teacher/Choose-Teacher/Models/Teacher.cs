using Choose_Teacher.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Choose_Teacher.Models
{
    public class Teacher:CommonProp
    {
        public int TeacherId { get; set; }
        [Required(ErrorMessage ="Input Teacher Name")]
        [Display(Name ="Teacher")]
        public string? TeacherName { get; set; }
        [Required(ErrorMessage = "Input Email")]
        [Display(Name = "Teacher Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Plase Input Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Input Phone Number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int phoneNumber { get; set; }
        [Required(ErrorMessage = "Choose City")]
        [Display(Name = "City")]
        public City City { get; set; }
        [Required(ErrorMessage = "Input Price Of Hour")]
        [Display(Name = "Price Of Hour")]
        public decimal PriceOfHour { get; set; } = 20;

        [Required(ErrorMessage = "Input Loacation")]
        [Display(Name = "Loacation")]
        public string? Loacation { get; set; }

        [Display(Name ="Bio")]
        public string? Bio { get; set; }
        [Display(Name ="Image")]
        [DataType (DataType.ImageUrl)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Choose Material")]
        [Display(Name = "Material")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<TeacherReview> TeacherReviews{ get; set; }
        public ICollection<TeacherAvailability> TeacherAvailabilities { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Certification> Certifications { get; set; }


        //public decimal Price(decimal Hour,decimal PriceOfHour)
        //{
        //    return Hour * PriceOfHour;  
        //}

    }
}
