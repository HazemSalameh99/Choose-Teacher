using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Plase Input Your Name")]
        [Display(Name = "Your Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Plase Input Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Plase Input Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Plase Confirm Password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Password Not Matching")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Choose City")]
        public City City { get; set; }

        [Required(ErrorMessage = "Choose The Role")]
        [Display(Name = "Role")]
        public Role Role { get; set; } = Role.User;

    }
}
