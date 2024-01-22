using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class About
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Input The Title")]
        [Display(Name ="Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Input The Description")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage ="Input Image")]
        public string Img { get; set; }

    }
}
