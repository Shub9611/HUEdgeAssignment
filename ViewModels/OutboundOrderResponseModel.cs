using DepotManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class OutboundOrderResponseModel
    {
        public OutboundOrder outboundOrder { get; set; }
        public bool? orderAccepted { get; set; }
        public string message { get; set; }
        public string DiscountMessage { get; set; }
    }

    public class TruckResponseModel
    {
        public Truck truck { get; set; }
        public string message { get; set; }
    }
}
