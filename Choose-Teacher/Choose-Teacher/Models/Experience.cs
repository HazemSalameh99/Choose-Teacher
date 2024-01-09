using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        [Required(ErrorMessage = "Input Organization")]
        [Display(Name = "Organization")]
        public string Organization { get; set; }
        [Required(ErrorMessage = "Input Position")]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Input StartDate")]
        [Display(Name = "StartDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "EndDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set;}
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
    }
}
