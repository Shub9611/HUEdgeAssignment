using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class WarehouseManagementViewModels
    {
    }

    public class UpdateProductInputViewModel
    {
        public long ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductDescription { get; set; }
    }

    public class UpdateLPNInputViewModel
    {
        public long LPNId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long NodeId { get; set; }
    }

    public class AllOrderStatusResponseModel
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public bool ShipmentStatus { get; set; }
        public string Status { get; set; }
    }
}
