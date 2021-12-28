using DepotManagementSystem.Models;
using DepotManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services.ServiceInterfaces
{
    public interface IOutboundOperationService
    {
        OutboundOrderResponseModel ReceiveCustomerOrder(OutboundOrder inputModel);
        string ManageShipment(long orderId);
    }
}
