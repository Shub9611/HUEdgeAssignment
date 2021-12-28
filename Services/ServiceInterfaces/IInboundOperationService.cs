using DepotManagementSystem.Models;
using DepotManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services.ServiceInterfaces
{
    public interface IInboundOperationService
    {
        InboundOrderResponseModel ReceiveOrders(InboundOrderInputViewModel inputModel);
        Pallet ModifyPalletQuantity(ModifyPalletQuantityInputModel inputModel);
        IEnumerable<NodeDistanceResponseModel> GetNodeDistance(long currentNodeId);
        MovePalletResponseModel MovePallet(long destNodeId, long palletId);
        List<PalletQuantityResponseModel> GetItemQuantity();
    }
}
