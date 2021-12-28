using DepotManagementSystem.Models;
using DepotManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services.ServiceInterfaces
{
    public interface ISystemManagementService
    {
        Product InsertProd(ProductInputViewModel input);
        Pallet InsertPallet(PalletInputViewModel input);
        Node InsertNode(NodeInputViewModel input);
        LPN InsertLPN(LPNInputViewModel input);
        NodeEdge InsertNodeEdges(NodeEdgeInputViewModel input);
    }
}
