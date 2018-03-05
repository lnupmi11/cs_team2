using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using xManik.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual Client Client { get; set; }
        public virtual Provider Provider { get; set; }
        public UserRole Role { get; set; }
    }

    public class Client
    {
        public string Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string ClientProperty { get; set; }
    }

    public class Provider
    {
        public string Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string ProviderProperty { get; set; }
    }

}
