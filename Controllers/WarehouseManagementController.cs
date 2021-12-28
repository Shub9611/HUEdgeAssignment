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
    public class WarehouseManagementController : ControllerBase
    {
        private readonly IWarehouseManagementService _warehouseManagementService;

        public WarehouseManagementController(IWarehouseManagementService warehouseManagementService)
        {
            _warehouseManagementService = warehouseManagementService;
        }

        /// <summary>
        /// 1.	View Pallets Details
        /// </summary>
        [HttpGet("GetPalletDetailsById")]
        public IActionResult GetPalletDetailsById(long id)
        {
            if (id == default(long) || id < default(long))
                return BadRequest("provide valid Id");

            var resp = _warehouseManagementService.GetPalletById(id);

            if (resp == null)
                return NotFound("couldn't find");

            return Ok(resp);
        }
        /// <summary>
        /// 1.	View Pallets Details
        /// </summary>
        [HttpGet("GetAllPalletDetails")]
        public IActionResult GetPalletDetails()
        {
            var resp = _warehouseManagementService.GetPalletDetails();

            if (resp == null)
                return NotFound("couldn't find");

            return Ok(resp);
        }
        /// <summary>
        /// 2.	View Product Details
        /// </summary>
        [HttpGet("GetProductDetailsById")]
        public IActionResult GetProductDetailsById(long id)
        {
            if (id == default(int) || id < default(long))
                return BadRequest("provide valid Id");

            var res = _warehouseManagementService.GetProductById(id);

            if(res == null)
                return NotFound("couldn't find");

            return Ok(res);
        }
        /// <summary>
        /// 2.	View Product Details
        /// </summary>
        [HttpGet("GetAllProductDetails")]
        public IActionResult GetProductDetails()
        {
            var res = _warehouseManagementService.GetProductDetails();

            if (res == null)
                return NotFound("couldn't find");

            return Ok(res);
        }
        /// <summary>
        /// 3.	View Product location
        /// </summary>
        [HttpGet("GetProductLocation")]
        public IActionResult GetProdLocation(long id)
        {
            var res = _warehouseManagementService.GetProdLocation(id);
            return Ok(res);
        }
        /// <summary>
        /// 4.	Update Product Information
        /// </summary>
        [HttpPost("UpdateProductDetails")]
        public IActionResult UpdateProduct(UpdateProductInputViewModel inputViewModel)
        {
            if (inputViewModel == null || inputViewModel.ProductId <= default(long))
                return BadRequest("please enter proper info");

            var res = _warehouseManagementService.UpdateProductInfo(inputViewModel);

            if (res == null)
                return NotFound("Couldn't find for the input prod id to update");

            return Ok(res);
        }
        /// <summary>
        /// 5.	View Orders status
        /// </summary>
        [HttpGet("ViewOutboundOrderStatus")]
        public IActionResult GetOutboundOrderStatus(long orderId)
        {
            var status = _warehouseManagementService.GetOutboundOrderStatus(orderId);

            return Ok(status);
        }
        /// <summary>
        /// 6.	Update/Manage LPN 
        /// </summary>
        [HttpPost("UpdateLPN")]
        public IActionResult UpdateLPN(UpdateLPNInputViewModel inputViewModel)
        {
            if(inputViewModel == null || inputViewModel.LPNId <= default(long))
                return BadRequest("please enter proper info");

            var res = _warehouseManagementService.UpdateLPNInfo(inputViewModel);

            if (res == null)
                return NotFound("Couldn't find for the input prod id to update");

            return Ok(res);
        }
        /// <summary>
        /// 7.	Search APIs for the Products. Searches by Type or Desc or Both
        /// </summary>
        [HttpGet("SearchProductByTypeOrDescription")]
        public IActionResult SearchProduct(string Type = "", string Desc = "")
        {
            if (String.IsNullOrEmpty(Type) && String.IsNullOrEmpty(Desc))
                return BadRequest("Atleast enter one");

            var res = _warehouseManagementService.SearchProduct(Type, Desc);

            if (!res.Any())
                return NotFound("Couldn't find based on the input");

            return Ok(res);
        }
        /// <summary>
        /// 8.	View all the orders and their status
        /// </summary>
        [HttpGet("ViewAllOrderStatus")]
        public IActionResult ViewAllOrderStatus()
        {
            var resp = _warehouseManagementService.GetStatusForAllOrders();

            if (!resp.Any())
                return NotFound("Couldn't find anything");

            return Ok(resp);
        }
    }
}
