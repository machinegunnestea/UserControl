using Microsoft.AspNetCore.Identity;

namespace UserControlling.Models
{
    public class User : IdentityUser
    {
        public string Name{ get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; }
    }
}
