using DepotManagementSystem.Models;
using DepotManagementSystem.Services.ServiceInterfaces;
using DepotManagementSystem.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services
{
    public class WarehouseManagementService : IWarehouseManagementService
    {
        private readonly IRepository<Pallet> _palletRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<ProductLocationResponseModel> _prodLocRespRepo;
        private readonly IRepository<Shipment> _shipmentRepo;
        private readonly IRepository<LPN> _lpnRepo;
        private readonly IRepository<OutboundOrder> _outboundOrderRepo;
        private readonly IUnitOfWork _uow;

        public WarehouseManagementService(IRepository<Pallet> palletRepo,
            IRepository<Product> productRepo,
            IRepository<ProductLocationResponseModel> prodLocRespRepo,
            IRepository<Shipment> shipmentRepo,
            IRepository<LPN> lpnRepo,
            IRepository<OutboundOrder> outboundOrderRepo,
            IUnitOfWork uow)
        {
            _palletRepo = palletRepo;
            _productRepo = productRepo;
            _prodLocRespRepo = prodLocRespRepo;
            _shipmentRepo = shipmentRepo;
            _lpnRepo = lpnRepo;
            _outboundOrderRepo = outboundOrderRepo;
            _uow = uow;
        }

        public Pallet GetPalletById(long id)
        {
            Pallet resp = new Pallet();

            resp = _palletRepo.GetByID(id);

            return resp;
        }

        public List<Pallet> GetPalletDetails()
        {
            var resp = _palletRepo.Get()?.ToList(); ;

            return resp;
        }

        public Product GetProductById(long id)
        {
            var resp = _productRepo.GetByID(id);

            return resp;
        }

        public List<Product> GetProductDetails()
        {
            var resp = _productRepo.Get()?.ToList(); ;

            return resp;
        }

        public IEnumerable<ProductLocationResponseModel> GetProdLocation(long prodId)
        {
            SqlParameter id = new SqlParameter("@ProductId", SqlDbType.BigInt);
            id.Value = prodId;
            var res = _prodLocRespRepo.GetWithRawSql("GetProductLocation @ProductId", id);
            return res;
        }

        public Product UpdateProductInfo(UpdateProductInputViewModel inputViewModel)
        {
            var prod = _productRepo.GetByID(inputViewModel.ProductId);
            if (prod == null)
                return null;

            prod.ProductType = String.IsNullOrEmpty(inputViewModel.ProductType) ? prod.ProductType : inputViewModel.ProductType;
            prod.ProductDescription = String.IsNullOrEmpty(inputViewModel.ProductDescription) ? prod.ProductDescription : inputViewModel.ProductDescription;

            var response = _productRepo.Update(prod);
            _uow.SaveChanges();

            return response;
        }

        public string GetOutboundOrderStatus(long id)
        {
            var shipmentLst = _shipmentRepo.Get(x => x.OrderId == id);

            bool shipped = shipmentLst.Where(x => x.ShipmentStatus == true).Any();

            return shipped ? "Shipped" : "Not Shipped";
        }

        public LPN UpdateLPNInfo(UpdateLPNInputViewModel inputViewModel)
        {
            var lpn = _lpnRepo.GetByID(inputViewModel.LPNId);
            if (lpn == null)
                return null;

            lpn.NodeId = inputViewModel.NodeId == default(long) ? lpn.NodeId : inputViewModel.NodeId;

            var response = _lpnRepo.Update(lpn);
            _uow.SaveChanges();

            return response;
        }

        public IEnumerable<Product> SearchProduct(string Type, string Desc)
        {
            SqlParameter prodType = new SqlParameter("@Type", SqlDbType.VarChar);
            SqlParameter prodDesc = new SqlParameter("@Desc", SqlDbType.VarChar);
            prodType.Value = Type;
            prodDesc.Value = Desc;

            var res = _productRepo.GetWithRawSql("SearchProduct @Type, @Desc", prodType, prodDesc);

            return res;
        }

        public List<AllOrderStatusResponseModel> GetStatusForAllOrders()
        {
            var allOrders = _outboundOrderRepo.Get().ToList();
            var allShipping = _shipmentRepo.Get().ToList();
            const string notShipped = "Not Shipped";
            const string shipped = "Shipped";
            AllOrderStatusResponseModel obj = new AllOrderStatusResponseModel();
            List<AllOrderStatusResponseModel> response = new List<AllOrderStatusResponseModel>();

            allOrders.ForEach(x =>
            {
                var shipmentObj = allShipping.Where(z => z.OrderId == x.OrderId)?.FirstOrDefault();
                if(shipmentObj == null)
                {
                    obj.OrderId = x.OrderId;
                    obj.ProductId = x.ProductId;
                    obj.ShipmentStatus = false;
                    obj.Status = notShipped;
                }
                else if(shipmentObj != null)
                {
                    obj.OrderId = x.OrderId;
                    obj.ProductId = x.ProductId;
                    obj.ShipmentStatus = shipmentObj.ShipmentStatus.Value;
                    obj.Status = shipmentObj.ShipmentStatus.Value ? shipped : notShipped;
                }
                response.Add(obj);
            });
            return response; 
        }
    }
}
