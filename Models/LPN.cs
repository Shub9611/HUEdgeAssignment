using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class LPN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LPNId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long NodeId { get; set; }
        [ForeignKey("NodeId")]
        public virtual Node Node { get; set; }
        public Pallet pallet { get; set; }
    }
}
