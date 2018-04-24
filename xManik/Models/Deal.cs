using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models
{
    public class Deal
    {
        public string DealId { get; set; }
        public string AssigmentId { get; set; }
        public string ClientId { get; set; }
        public string BloggerId { get; set; }
        public bool IsRead { get; set; }
    }
}
