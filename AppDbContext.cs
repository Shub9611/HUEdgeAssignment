using DepotManagementSystem.Models;
using DepotManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepotManagementSystem
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<LPN> LPNs { get; set; }
        public virtual DbSet<Pallet> Pallets { get; set; }
        public virtual DbSet<NodeEdge> NodeEdges { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }
        public virtual DbSet<OutboundOrder> OutboundOrders { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<InboundOrder> InboundOrders { get; set; }
        public virtual DbSet<ProductLocationResponseModel> ProductLocations { get; set; }
        public virtual DbSet<NodeDistanceResponseModel> NodeDistances { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
