using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class Certification
    {
        public int CertificationId { get; set; }
        [Required(ErrorMessage = "Input Certification Name")]
        [Display(Name = "Certification Name")]
        public string CertificationName { get; set;}
        [Required(ErrorMessage = "Input Issuing Authority")]
        [Display(Name = "Issuing Authority")]
        public string IssuingAuthority { get; set; }
        [Required(ErrorMessage = "Input Issue Date")]
        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }
    }
}
