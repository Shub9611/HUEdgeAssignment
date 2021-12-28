using DepotManagementSystem.Models;
using DepotManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services.ServiceInterfaces
{
    public interface IWarehouseManagementService
    {
        Pallet GetPalletById(long id);
        List<Pallet> GetPalletDetails();
        Product GetProductById(long id);
        List<Product> GetProductDetails();
        IEnumerable<ProductLocationResponseModel> GetProdLocation(long prodId);
        Product UpdateProductInfo(UpdateProductInputViewModel inputViewModel);
        string GetOutboundOrderStatus(long id);
        LPN UpdateLPNInfo(UpdateLPNInputViewModel inputViewModel);
        IEnumerable<Product> SearchProduct(string Type, string Desc);
        List<AllOrderStatusResponseModel> GetStatusForAllOrders();
    }
}
