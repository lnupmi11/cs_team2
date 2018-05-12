using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xBlogger.Models
{
    public class Chanel
    {
        public string ChanelId { get; set; }
        [ForeignKey("UserProfile")]
        public string UserProfileId { get; set; }

        public SocialNetworks Network { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public long SubscribersNum { get; set; }
        public long AvgViewNum { get; set; }
        public long AvgLikeNum { get; set; }
        public double LocalRank { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
