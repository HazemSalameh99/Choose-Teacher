using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models.SharedProp
{
    public class CommonProp
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
