using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class InboundOrderInputViewModel
    {
        //public long InboundOrderId { get; set; }
        public DateTime? InboundOrderDate { get; set; }
        public int? Quantity { get; set; }
        public long ProductId { get; set; }
    }
}
