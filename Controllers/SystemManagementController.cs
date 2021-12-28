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
    public class SystemManagementController : ControllerBase
    {
        private readonly ISystemManagementService _systemManagementService;

        public SystemManagementController(ISystemManagementService systemManagementService)
        {
            _systemManagementService = systemManagementService;
        }

        /// <summary>
        ///     1.	Define Products type for the warehouse
        /// </summary>
        [HttpPost("DefineProduct")]
        public IActionResult InsertProduct(ProductInputViewModel inputViewModel)
        {
            if(inputViewModel == null)
                return BadRequest("please provide input");

            var resp = _systemManagementService.InsertProd(inputViewModel);
            return Ok(resp);
        }
        /// <summary>
        /// 2.	Define Pallets items quantity
        /// 3.	Define Pallet with item type.
        /// </summary>
        [HttpPost("DefinePallet")]
        public IActionResult InsertPallet(PalletInputViewModel inputViewModel)
        {
            if (inputViewModel == null)
                return BadRequest("please provide input");

            var resp = _systemManagementService.InsertPallet(inputViewModel);
            return Ok(resp);
        }
        /// <summary>
        /// 4.	Defines Number of Nodes within warehouse
        /// </summary>
        [HttpPost("DefineNode")]
        public IActionResult InsertNode(NodeInputViewModel inputViewModel)
        {
            if (inputViewModel == null)
                return BadRequest("please provide input");

            var resp = _systemManagementService.InsertNode(inputViewModel);
            return Ok(resp);
        }
        /// <summary>
        /// 5.	Define LPN for Pallets
        /// </summary>
        [HttpPost("DefineLPN")]
        public IActionResult InsertLPN(LPNInputViewModel inputViewModel)
        {
            if (inputViewModel == null)
                return BadRequest("please provide input");

            var resp = _systemManagementService.InsertLPN(inputViewModel);
            return Ok(resp);
        }
        /// <summary>
        /// Define Edges(Distance between nodes)   
        /// </summary>
        [HttpPost("DefineNodeEdge")]
        public IActionResult InsertNodeEdge(NodeEdgeInputViewModel inputViewModel)
        {
            if (inputViewModel == null)
                return BadRequest("please provide input");

            var resp = _systemManagementService.InsertNodeEdges(inputViewModel);
            return Ok(resp);
        }
    }
}
