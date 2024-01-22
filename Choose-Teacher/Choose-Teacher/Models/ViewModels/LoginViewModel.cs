using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Plase Input Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Plase Input Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
