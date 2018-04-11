using System;
using System.Collections.Generic;
using System.Text;

namespace xManik.DAL.Entities
{
    public class PortfolioItem
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string ProviderId { get; set; }
    }
}
