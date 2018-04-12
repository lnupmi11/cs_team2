using System;

namespace xManik.DAL.Entities
{
    public class Order
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public string ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime OrderTime { get; set; }
        public string Details { get; set; }
    }
}
