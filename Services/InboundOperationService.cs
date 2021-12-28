using DepotManagementSystem.enums;
using DepotManagementSystem.Models;
using DepotManagementSystem.Services.ServiceInterfaces;
using DepotManagementSystem.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services
{
    public class InboundOperationService : IInboundOperationService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Pallet> _palletRepository;
        private readonly IRepository<InboundOrder> _inboundOrderRepository;
        private readonly IRepository<NodeDistanceResponseModel> _nodeDistanceRepo;
        private readonly IRepository<LPN> _lpnRepo;
        private readonly IUnitOfWork _uow;

        public InboundOperationService(IRepository<Product> productRepository,
            IRepository<Pallet> palletRepository,
            IRepository<InboundOrder> inboundOrderRepository,
            IRepository<NodeDistanceResponseModel> nodeDistanceRepo,
            IRepository<LPN> lpnRepo,
            IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _palletRepository = palletRepository;
            _inboundOrderRepository = inboundOrderRepository;
            _nodeDistanceRepo = nodeDistanceRepo;
            _lpnRepo = lpnRepo;
            _uow = uow;
        }

        public InboundOrderResponseModel ReceiveOrders(InboundOrderInputViewModel inputModel)
        {
            //Expression<Func<Pallet, bool>> expr = (a) => a.ProductId == inputModel.ProductId;
            InboundOrderResponseModel resp = new InboundOrderResponseModel();
            InboundOrder model = new InboundOrder();

            var pallet = _palletRepository.Get(a => a.ProductId == inputModel.ProductId)?.FirstOrDefault();
            string type = Convert.ToString((ProductTypeIds)inputModel.ProductId);

            int quantity = (int)(PalletQuantityEnum)System.Enum.Parse(typeof(PalletQuantityEnum), type);

            if(inputModel != null)
            {
                if (pallet != null)
                {
                    if (pallet.Quantity == quantity || inputModel.Quantity > quantity - pallet.Quantity)
                        return new InboundOrderResponseModel
                        {
                            orderAccepted = false,
                            message = String.Format("Please check capacity. Max capacity we have = {0}, capacity left to store = {1}", quantity, quantity - pallet.Quantity)
                        };

                    model.InboundOrderDate = DateTime.UtcNow;
                    model.ProductId = inputModel.ProductId;
                    model.Quantity = inputModel.Quantity;
                    
                    resp.inboundOrder = _inboundOrderRepository.Insert(model);
                    _uow.SaveChanges();
                    resp.orderAccepted = true;
                    resp.message = "order created";
                }
            }
            
            return resp;
        }

        public Pallet ModifyPalletQuantity(ModifyPalletQuantityInputModel inputModel)
        {
            var res = _palletRepository.Get(x => x.ProductId == inputModel.ProductId)?.FirstOrDefault();

            res.Quantity += inputModel.Quantity;

            var response = _palletRepository.Update(res);
            _uow.SaveChanges();

            return response;
        }

        public IEnumerable<NodeDistanceResponseModel> GetNodeDistance(long currentNodeId)
        {
            SqlParameter startNodeId = new SqlParameter("@StartNodeId", SqlDbType.BigInt);
            startNodeId.Value = currentNodeId;

            var res = _nodeDistanceRepo.GetWithRawSql("GetNodeDistance @StartNodeId", startNodeId);

            return res;
        }

        public MovePalletResponseModel MovePallet(long destNodeId, long palletId)
        {
            MovePalletResponseModel response = new MovePalletResponseModel();

            var pallet = _palletRepository.GetByID(palletId);
            var palletLpnId = pallet != null ? pallet.LPNId : default(long);
            if(palletLpnId <= default(long))
            {
                var palletLPN = _lpnRepo.GetByID(palletLpnId);
                if(palletLPN != null)
                {
                    var previousNodeId = palletLPN.NodeId;
                    palletLPN.NodeId = destNodeId;

                    var res = _lpnRepo.Update(palletLPN);
                    _uow.SaveChanges();

                    if (res != null)
                    {
                        response.NewNodeId = res.NodeId;
                        response.PalletId = pallet.PalletId;
                        response.PreviousNodeId = previousNodeId;
                    }
                }
            }
            return response;
        }

        public List<PalletQuantityResponseModel> GetItemQuantity()
        {
            var palletRes = _palletRepository.Get()?.ToList();
            var prodRes = _productRepository.Get()?.ToList();

            var resp = palletRes.Join(
                      prodRes,
                      palletRes => palletRes.ProductId,
                      prodRes => prodRes.ProductId,
                      (pallet, prod) => new PalletQuantityResponseModel 
                      {
                          PalletId = pallet.PalletId,
                          ProductId = pallet.ProductId,
                          ProductType = prod.ProductType,
                          Quantity = pallet.Quantity
                      })?.ToList();
            
            return resp;
        }
    }
}