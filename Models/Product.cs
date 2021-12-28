using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductDescription { get; set; }
        public IEnumerable<Pallet> Pallet { get; set; }
        public IEnumerable<OutboundOrder> OutboundOrders { get; set; }
        public IEnumerable<InboundOrder> InboundOrders { get; set; }
    }
}
