using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UserControlling.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Passwords don't match")]
        [Display(Name ="Confirm password")]
        public string ConfirmPassword { get; set; }


    }
}
