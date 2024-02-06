using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models.ViewModels
{
    public class TeacherViewModel
    {
        [Required(ErrorMessage = "Input Teacher Name")]
        [Display(Name = "Teacher")]
        public string? TeacherName { get; set; }
        [Required(ErrorMessage = "Input Email")]
        [Display(Name = "Teacher Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Plase Input Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Plase Confirm Password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password Not Matching")]
        [DataType(DataType.Password)]
        public string? confirmPassword { get; set; }
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

        [Required(ErrorMessage = "Choose Material")]
        [Display(Name = "Material")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
