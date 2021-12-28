using DepotManagementSystem.Models;
using DepotManagementSystem.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutboundOperationController : ControllerBase
    {
        private readonly IOutboundOperationService _outboundOperationService;

        public OutboundOperationController(IOutboundOperationService outboundOperationService)
        {
            _outboundOperationService = outboundOperationService;
        }

        /// <summary>
        /// Generals
        /// 1.	Receive orders from customers.
        /// 2.	Override Items priority
        /// 3.	Provide discounts and offers for the customer
        /// Order
        /// 1.	Approve/Reject the order
        /// 3.	Manage discounts on the orders.
        /// </summary>
        [HttpPost("ReceiveOrder")]
        public IActionResult ReceiveOrder(OutboundOrder input)
        {
            if (input == null)
                return BadRequest("please provide input");

            var res = _outboundOperationService.ReceiveCustomerOrder(input);

            if (res == null)
                return BadRequest("something went wrong");

            return Ok(res);
        }
        /// <summary>
        /// Order Planning
        /// 1.	See items ready for the shipment
        /// 2.	Assign trucks for the shipment
        /// Shipping
        /// 1.	Schedule time and day for the shipment
        /// 2.	Manage status of the shipment
        /// </summary>
        [HttpPost("OrderPlanning")]
        public IActionResult OrderPlanning(long orderId)
        {
            if (orderId <= default(long))
                return BadRequest("Please enter valid orderId");

            var res = _outboundOperationService.ManageShipment(orderId);

            return Ok(res);
        }

    }
}
