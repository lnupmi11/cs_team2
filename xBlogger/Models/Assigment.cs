﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace xBlogger.Models
{
    public class Assigment
    {
        public string AssigmentId { get; set; }
        [ForeignKey("UserProfile")]
        public string UserProfileId { get; set; }

        public SocialNetworks Network { get; set; }
        public AdTypes Type { get; set; }
        public AdFormat Format { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public long MaxBudget { get; set; }
        public DateTime Deadline { get; set; }
        ICollection<Chanel> Chanels { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
