using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Input Material Name")]
        [Display(Name = "Material")]
        public string CategoryName { get; set; }
        [Display(Name = "Material Descrition")]
        [DataType(DataType.MultilineText)]
        public string? CategoryDesc { get; set; }
        [Display(Name = "Material Images")]
        [DataType(DataType.ImageUrl)]
        public IFormFile? CategoryImg { get; set; }
    }
}
