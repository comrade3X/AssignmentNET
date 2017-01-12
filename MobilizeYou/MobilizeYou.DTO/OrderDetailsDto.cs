using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilizeYou.DTO
{
    public class OrderDetailsDto
    {
        public System.DateTime ValidFrom { get; set; }
        public System.DateTime ValidTo { get; set; }

        public virtual Order Order { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Product Product { get; set; }

        public virtual Employee Employee { get; set; }

        public string Status { get; set; }

        public int OrderDetailId { get; set; }

        public string Type { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Customer { get; set; }

        public string OrderDate { get; set; }

        public string ProductName { get; set; }
    }
}
