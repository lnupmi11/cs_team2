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
        public Assigment Assigment { get; set; }
        public string ChanelId { get; set; }
        public Chanel Chanel { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsRead { get; set; }
    }
}
