using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Models
{
    public class Node
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NodeId { get; set; }
        public string NodeName { get; set; }
        public string NodeType { get; set; }
        public string Zone { get; set; }
        public IEnumerable<LPN> LPNs { get; set; }
        [InverseProperty("StartingNode")]
        public IEnumerable<NodeEdge> StartingNode { get; set; }
        [InverseProperty("EndingNode")]
        public IEnumerable<NodeEdge> EndingNode { get; set; }
    }
}
