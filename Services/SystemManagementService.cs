using DepotManagementSystem.Models;
using DepotManagementSystem.Services.ServiceInterfaces;
using DepotManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem.Services
{
    public class SystemManagementService : ISystemManagementService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Pallet> _palletRepository;
        private readonly IRepository<Node> _nodeRepository;
        private readonly IRepository<LPN> _lpnRepository;
        private readonly IRepository<NodeEdge> _nodeEdgeRepository;
        private readonly IUnitOfWork _uow;
        public SystemManagementService(IRepository<Product> productRepository,
            IRepository<Pallet> palletRepository,
            IRepository<Node> nodeRepository,
            IRepository<LPN> lpnRepository,
            IRepository<NodeEdge> nodeEdgeRepository,
            IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _palletRepository = palletRepository;
            _nodeRepository = nodeRepository;
            _lpnRepository = lpnRepository;
            _nodeEdgeRepository = nodeEdgeRepository;
            _uow = uow;
        }
        
        public Product InsertProd(ProductInputViewModel input)
        {
            Product model = new Product();
            model.ProductDescription = input.ProductDescription;
            model.ProductType = input.ProductType;

            var resp = _productRepository.Insert(model);
            _uow.SaveChanges();
            return resp;
        }
        
        public Pallet InsertPallet(PalletInputViewModel input)
        {
            Pallet model = new Pallet();
            model.CreatedDate = DateTime.UtcNow;
            model.LPNId = input.LPNId;
            model.ProductId = input.ProductId;
            model.Quantity = input.Quantity;

            var resp = _palletRepository.Insert(model);
            _uow.SaveChanges();
            return resp;
        }
        
        public Node InsertNode(NodeInputViewModel input)
        {
            Node model = new Node();
            model.NodeName = input.NodeName;
            model.NodeType = input.NodeType;
            model.Zone = input.Zone;

            var resp = _nodeRepository.Insert(model);
            _uow.SaveChanges();
            return resp;
        }

        public LPN InsertLPN(LPNInputViewModel input)
        {
            LPN model = new LPN();
            model.NodeId = input.NodeId;
            model.CreatedDate = DateTime.UtcNow;

            var resp = _lpnRepository.Insert(model);
            _uow.SaveChanges();
            return resp;
        }

        public NodeEdge InsertNodeEdges(NodeEdgeInputViewModel input)
        {
            NodeEdge model = new NodeEdge();
            model.EdgeLength = input.EdgeLength;
            model.StartNode = input.StartNode;
            model.EndNode = input.EndNode;

            var resp = _nodeEdgeRepository.Insert(model);
            _uow.SaveChanges();
            return resp;
        }
    }
}
