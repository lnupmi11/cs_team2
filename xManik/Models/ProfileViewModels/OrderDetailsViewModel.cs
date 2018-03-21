using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models.ProfileViewModels
{
    public class OrderDetailsViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CustomerRegistered { get; set; }

        public string ServiceDescription { get; set; }
        public double ServicePrice { get; set; }
        public DateTime ServicePosted { get; set; }
        
        public string AdditionalServiceInfo { get; set; }
        public DateTime ServiceStartTime { get; set; }
        public DateTime ServiceEndTime { get; set; }
    }
}
