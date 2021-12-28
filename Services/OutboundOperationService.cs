using DepotManagementSystem.enums;
using DepotManagementSystem.Models;
using DepotManagementSystem.Services.ServiceInterfaces;
using DepotManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services
{
    public class OutboundOperationService : IOutboundOperationService
    {
        private readonly IRepository<OutboundOrder> _outboundOrderRepo;
        private readonly IRepository<Pallet> _palletRepository;
        private readonly IRepository<Truck> _truckRepo;
        private readonly IRepository<Shipment> _shipmentRepo;
        private readonly IUnitOfWork _uow;

        public OutboundOperationService(IRepository<OutboundOrder> outboundOrderRepo,
            IRepository<Pallet> palletRepository,
            IRepository<Truck> truckRepo,
            IRepository<Shipment> shipmentRepo,
            IUnitOfWork uow)
        {
            _outboundOrderRepo = outboundOrderRepo;
            _palletRepository = palletRepository;
            _truckRepo = truckRepo;
            _shipmentRepo = shipmentRepo;
            _uow = uow;
        }

        public OutboundOrderResponseModel ReceiveCustomerOrder(OutboundOrder inputModel)
        {
            OutboundOrderResponseModel resp = new OutboundOrderResponseModel();
            OutboundOrder model = new OutboundOrder();

            var pallet = _palletRepository.Get(a => a.ProductId == inputModel.ProductId)?.FirstOrDefault();
            //string type = Convert.ToString((ProductTypeIds)inputModel.ProductId);

            //int quantity = (int)(PalletQuantityEnum)System.Enum.Parse(typeof(PalletQuantityEnum), type);

            if (inputModel != null)
            {
                if (pallet != null)
                {
                    if (inputModel.Quantity > pallet.Quantity)
                        return new OutboundOrderResponseModel
                        {
                            orderAccepted = false,
                            message = String.Format("Please check order quantity. stock we have left = {0}", pallet.Quantity)
                        };

                    model.OrderDate = DateTime.UtcNow;
                    model.ProductId = inputModel.ProductId;
                    model.Quantity = inputModel.Quantity;
                    decimal discount = CalculateDiscount(inputModel.ProductId, inputModel.Quantity.Value, inputModel.Cost.Value);
                    model.Cost = inputModel.Cost - discount;

                    resp.outboundOrder = _outboundOrderRepo.Insert(model);
                    _uow.SaveChanges();
                    resp.orderAccepted = true;
                    resp.message = "order created";
                    resp.DiscountMessage = discount != default(decimal) ? 
                        String.Format("Yayy! You have received a discount of {0} on original cost", discount)
                        : String.Empty;

                    //call CrossDockApi to modify pallet quantity
                }
            }

            return resp;
        }

        private decimal CalculateDiscount(long prodId, int quantity, decimal cost)
        {
            string prodType = Convert.ToString((ProductTypeIds)prodId).ToLower();
            decimal discountAmt = default(decimal);
            int quantityForDisc = default(int);
            decimal discountPercent = default(decimal);

            switch (prodId)
            {
                case (long)ProductTypeIds.Mobile:
                    quantityForDisc = (int)(DiscountQuantity)System.Enum.Parse(typeof(DiscountQuantity), prodType);
                    if(quantity >= quantityForDisc)
                    {
                        discountPercent = (int)(DiscountPercentage)System.Enum.Parse(typeof(DiscountPercentage), prodType);
                        discountAmt = discountPercent != default(decimal) ? cost * (discountPercent / 100) : default(decimal);
                    }
                    break;

                case (long)ProductTypeIds.Laptop:
                    quantityForDisc = (int)(DiscountQuantity)System.Enum.Parse(typeof(DiscountQuantity), prodType);
                    if (quantity >= quantityForDisc)
                    {
                        discountPercent = (int)(DiscountPercentage)System.Enum.Parse(typeof(DiscountPercentage), prodType);
                        discountAmt = cost * (discountPercent / 100);
                    }
                    break;

                case (long)ProductTypeIds.Mouse:
                    quantityForDisc = (int)(DiscountQuantity)System.Enum.Parse(typeof(DiscountQuantity), prodType);
                    if (quantity >= quantityForDisc)
                    {
                        discountPercent = (int)(DiscountPercentage)System.Enum.Parse(typeof(DiscountPercentage), prodType);
                        discountAmt = cost * (discountPercent / 100);
                    }
                    break;

                case (long)ProductTypeIds.HeadPhone:
                    quantityForDisc = (int)(DiscountQuantity)System.Enum.Parse(typeof(DiscountQuantity), prodType);
                    if (quantity >= quantityForDisc)
                    {
                        discountPercent = (int)(DiscountPercentage)System.Enum.Parse(typeof(DiscountPercentage), prodType);
                        discountAmt = cost * (discountPercent / 100);
                    }
                    break;
            }

            return discountAmt;
        }

        public string ManageShipment(long orderId)
        {
            //check if order exists
            if (_outboundOrderRepo.GetByID(orderId) == null)
                return "Order was never created/accepted. Create order first";

            var firstAvailableTruck = _truckRepo.Get(x => x.Available == true)?.FirstOrDefault();

            if (firstAvailableTruck == null)
                return "No Trucks available to assign this order for shipment";

            Shipment shipment = new Shipment();
            shipment.OrderId = orderId;
            shipment.TruckId = firstAvailableTruck.TruckId;

            var res = _shipmentRepo.Insert(shipment);
            _uow.SaveChanges();

            if (res == null)
                return "something went wrong while creating shipment record";

            //After Shipment is created, increase the shipment cargo count this truck has assigned
            var truckdata = updateTruckShipmentCnt(res.TruckId);

            if(truckdata?.truck != null)
            {
                if (truckdata.truck?.ShipmentItemCount == 10)
                    _shipmentRepo.GetWithRawSql("Update [dbo].[Shipments] set [ShipmentStatus] = 1, [ShipmentDate] = GETUTCDATE() where [TruckId] = {0)", truckdata.truck.TruckId);
            }

            return "Shipment Created";
        }

        private TruckResponseModel updateTruckShipmentCnt(long truckId)
        {
            TruckResponseModel response = new TruckResponseModel();

            var truck = _truckRepo.GetByID(truckId);

            if (truck == null)
                response.message = "couldn't find truck";

            if(truck.ShipmentItemCount < 10)
            {
                truck.ShipmentItemCount += 1;
            }

            if(truck.ShipmentItemCount == 10)
            {
                truck.Available = false;
            }

            var res = _truckRepo.Update(truck);
            _uow.SaveChanges();

            if (res == null)
                response.message =  "Something went wrong";

            response.truck = res;
            response.message = "Incremented shipment count by 1 for truckId =" + res.TruckId;

            return response;
        }
    }
}
