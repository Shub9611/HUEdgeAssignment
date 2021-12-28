using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class SystemManagementViewModel
    {
    }
    public class ProductInputViewModel
    {
        public string ProductType { get; set; }
        public string ProductDescription { get; set; }
    }
    public class PalletInputViewModel
    {
        public int Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ProductId { get; set; }
        public long LPNId { get; set; }
    }
    public class NodeInputViewModel
    {
        public string NodeName { get; set; }
        public string NodeType { get; set; }
        public string Zone { get; set; }
    }
    public class LPNInputViewModel
    {
        public DateTime? CreatedDate { get; set; }
        public long NodeId { get; set; }
    }
    public class NodeEdgeInputViewModel
    {
        public int EdgeLength { get; set; }
        public long StartNode { get; set; }
        public long? EndNode { get; set; }
    }
}
