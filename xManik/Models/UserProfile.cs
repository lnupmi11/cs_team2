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
        public string ImageName { get; set; }
        public DateTime DateRegistered { get; set; }

        public virtual ICollection<Assigment> Assigments { get; set; }
        public virtual ICollection<Chanel> Chanels { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
