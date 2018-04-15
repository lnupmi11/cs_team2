using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xManik.Models
{
    public class Chanel
    {
        public string ChanelId { get; set; }
        [ForeignKey("UserProfile")]
        public string BloggerProfileId { get; set; }

        public SocialNetworks Network { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public long SubscribersNum { get; set; }
        public long AvgViewNum { get; set; }
        public long AvgLikeNum { get; set; }
        public double LocalRank { get; set; }

        public virtual UserProfile BloggerProfile { get; set; }
    }
}
