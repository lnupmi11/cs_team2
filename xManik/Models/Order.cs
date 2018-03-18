using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string ProviderId { get; set; }
        public string ServiceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsRead { get; set; }
    }
}
