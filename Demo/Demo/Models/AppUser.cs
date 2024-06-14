using Microsoft.AspNetCore.Identity;

namespace Demo.Models
{
    public class AppUser : IdentityUser
    {
        public Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
