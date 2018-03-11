using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace xManik.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual Client Client { get; set; }
        public virtual Provider Provider { get; set; }
        public UserRole Role { get; set; }
        public byte[] ProfileImage { get; set; }
        public DateTime DateRegistered { get; set; }
    }

    public class Client
    {
        public string Id { get; set; }
        //Credit card info
        public virtual ApplicationUser User { get; set; }
        public string ClientProperty { get; set; }
    }

    public class Provider
    { 
        public string Id { get; set; }
        public virtual Marker Marker { get; set; }
        //Credit card info
        [Display(Name = "Додайте інформацію про себе")]
        public string Description { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Artwork> Portfolio { get; set; }
        //public virtual ICollection<Review> Reviews { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string ProviderProperty { get; set; }
    }
}
