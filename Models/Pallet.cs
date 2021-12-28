using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class Pallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PalletId { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public long LPNId { get; set; }
        [ForeignKey("LPNId")]
        public virtual LPN LPN { get; set; }
    }
}
