using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class NodeEdge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EdgeId { get; set; }
        public int EdgeLength { get; set; }
        public long StartNode { get; set; }
        public long? EndNode { get; set; }
        [ForeignKey("StartNode")]
        public virtual Node StartingNode { get; set; }
        [ForeignKey("EndNode")]
        public virtual Node EndingNode { get; set; }
    }
}
