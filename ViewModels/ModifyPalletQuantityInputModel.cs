using DepotManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class ModifyPalletQuantityInputModel
    {
        public bool orderAccepted { get; set; }
        public int Quantity { get; set; }
        public long ProductId { get; set; }
    }

    public class ModifyPalletQuantityResponseModel
    {
        public Pallet palletObj { get; set; }
        public string message { get; set; }
    }
}
