using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class Truck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TruckId { get; set; }
        public bool? Available { get; set; }
        public int? ShipmentItemCount { get; set; }
        public Shipment Shipment { get; set; }
    }
}
