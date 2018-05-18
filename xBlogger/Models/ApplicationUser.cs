using Microsoft.AspNetCore.Identity;

namespace xBlogger.Models
{
    // Add UserProfile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}
