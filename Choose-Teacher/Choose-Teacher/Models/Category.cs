using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Input Category Name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Descrition")]
        [DataType(DataType.MultilineText)]
        public string? CategoryDesc { get; set; }
        [Display(Name = "Category Images")] 
        [DataType(DataType.ImageUrl)]
        public string? CategoryImg { get; set; }
    }
}
