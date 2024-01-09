using System.ComponentModel.DataAnnotations;

namespace Choose_Teacher.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="Plase Input The Name")]
        [Display(Name ="Your Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Plase Input Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Plase Input Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Display(Name ="Image")]
        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }
        [Display(Name ="Choose City")]
        public City City { get; set; }
        public string? Loacation { get; set; }
        
        [Required(ErrorMessage ="Choose The Role")]
        [Display(Name ="Role")]
        public Role Role { get; set; } = Role.User;

    }
    public enum Role
    {
        Admin,
        User,
        Teacher
    }
    public enum City
    {
        Amman,
        Irbid,
        Zarqa
    }
}
