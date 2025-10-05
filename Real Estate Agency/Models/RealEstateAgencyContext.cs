using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Real_Estate_Agency.Models;

public partial class RealEstateAgencyContext : DbContext
{
    public RealEstateAgencyContext(DbContextOptions<RealEstateAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Deal> Deals { get; set; }
    public virtual DbSet<Owner> Owners { get; set; }
    public virtual DbSet<Property> Properties { get; set; }
    public virtual DbSet<Showing> Showings { get; set; }
    public virtual DbSet<ViewAgentSalesReport> ViewAgentSalesReports { get; set; }
    public virtual DbSet<ViewAllDeal> ViewAllDeals { get; set; }
    public virtual DbSet<ViewAverageSaleTime> ViewAverageSaleTimes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PK__Agents__9AC3BFD112228E52");
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.AgentContacts).HasMaxLength(255);
            entity.Property(e => e.AgentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A042F0AA7D6");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientContacts).HasMaxLength(255);
            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.Property(e => e.ClientRequirements).HasMaxLength(255);
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.HasKey(e => e.DealId).HasName("PK__Deals__E5B28146C8ECE45E");
            entity.Property(e => e.DealId).HasColumnName("DealID");
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.DealAmount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

            entity.HasOne(d => d.Agent).WithMany(p => p.Deals)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_Deal_Agent");

            entity.HasOne(d => d.Client).WithMany(p => p.Deals)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Deal_Client");

            entity.HasOne(d => d.Property).WithMany(p => p.Deals)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK_Deal_Property");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owners__81938598A834A120");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.OwnerContacts).HasMaxLength(255);
            entity.Property(e => e.OwnerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Properti__70C9A75596D4F70A");
            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.PropertyAddress).HasMaxLength(255);
            entity.Property(e => e.PropertyDescription).HasMaxLength(255);
            entity.Property(e => e.PropertyPhoto).HasMaxLength(255);
            entity.Property(e => e.PropertyPrice).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.PropertyStatus).HasMaxLength(50);
            entity.Property(e => e.PropertyType).HasMaxLength(50);

            entity.HasOne(d => d.Owner).WithMany(p => p.Properties)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_Property_Owner");
        });

        modelBuilder.Entity<Showing>(entity =>
        {
            entity.HasKey(e => e.ShowingId).HasName("PK__Showings__995FC057AB2CD705");
            entity.Property(e => e.ShowingId).HasColumnName("ShowingID");
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");
            entity.Property(e => e.ShowingDateTime).HasColumnType("datetime");
            entity.Property(e => e.ShowingResult).HasMaxLength(255);

            entity.HasOne(d => d.Agent).WithMany(p => p.Showings)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_Showing_Agent");

            entity.HasOne(d => d.Client).WithMany(p => p.Showings)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Showing_Client");

            entity.HasOne(d => d.Property).WithMany(p => p.Showings)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK_Showing_Property");
        });

        modelBuilder.Entity<ViewAgentSalesReport>(entity =>
        {
            entity.HasNoKey().ToView("View_AgentSalesReport");
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.AgentName).HasMaxLength(100);
            entity.Property(e => e.AverageDealAmount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TotalSalesAmount).HasColumnType("decimal(15, 2)");
        });

        modelBuilder.Entity<ViewAllDeal>(entity =>
        {
            entity.HasNoKey().ToView("View_AllDeals");
            entity.Property(e => e.AgentContacts).HasMaxLength(255);
            entity.Property(e => e.AgentId).HasColumnName("AgentID");
            entity.Property(e => e.AgentName).HasMaxLength(100);
            entity.Property(e => e.ClientContacts).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.Property(e => e.DealAmount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.DealId).HasColumnName("DealID");
            entity.Property(e => e.OwnerContacts).HasMaxLength(255);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.OwnerName).HasMaxLength(100);
            entity.Property(e => e.PropertyAddress).HasMaxLength(255);
            entity.Property(e => e.PropertyId).HasColumnName("PropertyID");
            entity.Property(e => e.PropertyPrice).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.PropertyStatus).HasMaxLength(50);
            entity.Property(e => e.PropertyType).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewAverageSaleTime>(entity =>
        {
            entity.HasNoKey().ToView("View_AverageSaleTime");
            entity.Property(e => e.AverageDaysToSale).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}