using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ShipmentId { get; set; }
        public bool? ShipmentStatus { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public long TruckId { get; set; }
        [ForeignKey("TruckId")]
        public virtual Truck Truck { get; set; }
        public long OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual OutboundOrder OutboundOrder { get; set; }
    }
}
