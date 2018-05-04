using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models.UserProfileViewModels
{
    public class ChanelViewModel
    {
        public SocialNetworks Network { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
    }
}
