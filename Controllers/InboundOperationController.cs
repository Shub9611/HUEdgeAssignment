using DepotManagementSystem.Services.ServiceInterfaces;
using DepotManagementSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InboundOperationController : ControllerBase
    {
        private readonly IInboundOperationService _inboundOperationService;

        public InboundOperationController(IInboundOperationService inboundOperationService)
        {
            _inboundOperationService = inboundOperationService;
        }

        /// <summary>
        /// 1.	Create a dummy order
        /// 2.	Verify Order Id
        /// 3.	Accept/Reject the order
        /// </summary>
        [HttpPost("Receive")]
        public IActionResult ReceiveApi(InboundOrderInputViewModel input)
        {
            if (input == null)
                return BadRequest("Please enter proper input");
            var resp = _inboundOperationService.ReceiveOrders(input);
            if (resp.orderAccepted == true)
                return Ok(resp);

            return BadRequest(resp?.message);
        }

        /// <summary>
        /// 1.	Modify the Pallets quantities
        /// 2.	Assign the priority to the item.
        /// 3.	Assign items to pallets.
        /// </summary>
        [HttpPost("CrossDock")]
        public IActionResult CrossDockApi(ModifyPalletQuantityInputModel inputModel)
        {
            if(inputModel == null)
                return BadRequest("Please enter proper input");
            if (inputModel.orderAccepted == false)
                return BadRequest("This order was not accepted. Please use ReceiveApi to create valid order again");

            var res = _inboundOperationService.ModifyPalletQuantity(inputModel);

            if (res == null)
                return BadRequest("Something went wrong");

            return Ok(res);
        }

        /// <summary>
        /// Gets distance to all nodes from the current node
        /// </summary>
        [HttpGet("GetDistanceBetweenNodes")]
        public IActionResult GetDistanceBetweenNodes(long currentNodeId)
        {
            var res = _inboundOperationService.GetNodeDistance(currentNodeId);

            if (!res.Any())
                return NotFound("couldn't find anything");

            return Ok(res);
        }
        /// <summary>
        /// 1.	Move Items/pallets within warehouse
        /// Refer GetDistanceBetweenNodes to get the distance between nodes if required for reference.
        /// </summary>
        [HttpPost("PutAway")]
        public IActionResult PutAwayApi(long destNodeId, long palletId)
        {
            if (destNodeId <= default(long) || palletId <= default(long))
                return BadRequest("Please enter both id's");

            var res = _inboundOperationService.MovePallet(destNodeId, palletId);

            if (res == null)
                return BadRequest("Couldn't update");

            return Ok(res);
        }
        /// <summary>
        /// 2.	Api to find out Quantities of items are available in the warehouse.
        /// </summary>
        [HttpGet("GetPalletQuantity")]
        public IActionResult GetPalletQuantity()
        {
            var res = _inboundOperationService.GetItemQuantity();

            if (!res.Any())
                return NotFound("Couldn't find anything");

            return Ok(res);
        }
    }
}
