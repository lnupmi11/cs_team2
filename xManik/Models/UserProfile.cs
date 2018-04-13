using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xManik.Models
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        //public byte[] ProfileImage { get; set; }
        public string ImageName { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<PortfolioItem> Portfolio { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
