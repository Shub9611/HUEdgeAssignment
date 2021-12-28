using DepotManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class InboundOrderResponseModel
    {
        public InboundOrder inboundOrder { get; set; }
        public bool? orderAccepted { get; set; }
        public string message { get; set; }
    }

    public class NodeDistanceResponseModel
    {
        public long Id { get; set; }
        public long StartNodeId { get; set; }
        public string StartNodeName { get; set; }
        public long EndNodeId { get; set; }
        public string EndNodeName { get; set; }
        public int Distance { get; set; }
    }

    public class MovePalletResponseModel
    {
        public long PalletId { get; set; }
        public long PreviousNodeId { get; set; }
        public long NewNodeId { get; set; }
    }

    public class PalletQuantityResponseModel
    {
        public long PalletId { get; set; }
        public long ProductId { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
