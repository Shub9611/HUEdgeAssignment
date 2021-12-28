﻿// <auto-generated />
using System;
using DepotManagementSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DepotManagementSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211226193103_Added_OutboundOrder_Tbl")]
    partial class Added_OutboundOrder_Tbl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DepotManagementSystem.Models.LPN", b =>
                {
                    b.Property<long>("LPNId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("NodeId")
                        .HasColumnType("bigint");

                    b.HasKey("LPNId");

                    b.HasIndex("NodeId");

                    b.ToTable("LPNs");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Node", b =>
                {
                    b.Property<long>("NodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NodeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NodeId");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.NodeEdge", b =>
                {
                    b.Property<long>("EdgeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EdgeLength")
                        .HasColumnType("int");

                    b.Property<long?>("EndNode")
                        .HasColumnType("bigint");

                    b.Property<long>("StartNode")
                        .HasColumnType("bigint");

                    b.HasKey("EdgeId");

                    b.HasIndex("EndNode");

                    b.HasIndex("StartNode");

                    b.ToTable("NodeEdges");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.OutboundOrder", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OutboundOrders");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Pallet", b =>
                {
                    b.Property<long>("PalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("LPNId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PalletId");

                    b.HasIndex("LPNId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("Pallets");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Truck", b =>
                {
                    b.Property<long>("TruckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Available")
                        .HasColumnType("bit");

                    b.Property<int?>("ShipmentItemCount")
                        .HasColumnType("int");

                    b.HasKey("TruckId");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.LPN", b =>
                {
                    b.HasOne("DepotManagementSystem.Models.Node", "Node")
                        .WithMany("LPNs")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Node");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.NodeEdge", b =>
                {
                    b.HasOne("DepotManagementSystem.Models.Node", "EndingNode")
                        .WithMany("EndingNode")
                        .HasForeignKey("EndNode");

                    b.HasOne("DepotManagementSystem.Models.Node", "StartingNode")
                        .WithMany("StartingNode")
                        .HasForeignKey("StartNode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndingNode");

                    b.Navigation("StartingNode");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.OutboundOrder", b =>
                {
                    b.HasOne("DepotManagementSystem.Models.Product", "Product")
                        .WithMany("outboundOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Pallet", b =>
                {
                    b.HasOne("DepotManagementSystem.Models.LPN", "LPN")
                        .WithOne("pallet")
                        .HasForeignKey("DepotManagementSystem.Models.Pallet", "LPNId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DepotManagementSystem.Models.Product", "Product")
                        .WithMany("Pallet")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LPN");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.LPN", b =>
                {
                    b.Navigation("pallet");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Node", b =>
                {
                    b.Navigation("EndingNode");

                    b.Navigation("LPNs");

                    b.Navigation("StartingNode");
                });

            modelBuilder.Entity("DepotManagementSystem.Models.Product", b =>
                {
                    b.Navigation("outboundOrders");

                    b.Navigation("Pallet");
                });
#pragma warning restore 612, 618
        }
    }
}
