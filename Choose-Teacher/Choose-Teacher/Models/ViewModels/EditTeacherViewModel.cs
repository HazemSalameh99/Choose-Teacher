using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models.ViewModels
{
    public class EditTeacherViewModel
    {
        [Required(ErrorMessage = "Input Teacher Name")]
        [Display(Name = "Teacher")]
        public string? TeacherName { get; set; }
        [Required(ErrorMessage = "Input Email")]
        [Display(Name = "Teacher Email")]
        public string? Email { get; set; }
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

        [Display(Name = "Bio")]
        public string? Bio { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public IFormFile? ImageUrl { get; set; }
        [Required(ErrorMessage = "Choose Material")]
        [Display(Name = "Material")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
