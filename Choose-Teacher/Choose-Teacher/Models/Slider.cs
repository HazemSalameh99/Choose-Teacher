using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class Slider
    {
        public int SliderId { get; set; }
        [Required(ErrorMessage ="Input Slider Title")]
        [Display(Name ="Slider Title")]
        [DataType(DataType.Text)]   
        public string Title { get; set; }
        [Required(ErrorMessage = "Input Slider Sub Title")]
        [Display(Name = "Slider Sub Title")]
        [DataType(DataType.Text)]
        public string SubTitle { get; set; }
        [Display(Name ="Image")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

    }
}
