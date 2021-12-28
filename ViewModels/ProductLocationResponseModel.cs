using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.ViewModels
{
    public class ProductLocationResponseModel
    {
        [Key]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductType { get; set; }
        //location
        public long NodeId { get; set; }
        public string NodeName { get; set; }
        public string NodeType { get; set; }
        public string Zone { get; set; }
    }
}
