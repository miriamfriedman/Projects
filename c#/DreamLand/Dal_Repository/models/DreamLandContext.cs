﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal_Repository.models;

public partial class DreamLandContext : DbContext
{
    public DreamLandContext()
    {
    }

    public DreamLandContext(DbContextOptions<DreamLandContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopDetail> ShopDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Categori__6A1C8AFAE1A4E06E");

            entity.Property(e => e.CatId).ValueGeneratedNever();
            entity.Property(e => e.CatName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompId).HasName("PK__Companie__AD362A16E2EE0CD6");

            entity.Property(e => e.CompId).ValueGeneratedNever();
            entity.Property(e => e.CompName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__Customer__049E3AA9AC47F39C");

            entity.Property(e => e.CustId).ValueGeneratedNever();
            entity.Property(e => e.CustEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PK__Products__042785E55A9D2D6E");

            entity.Property(e => e.ProdId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProdName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CatCodeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatCode)
                .HasConstraintName("FK__Products__CatCod__3B75D760");

            entity.HasOne(d => d.CompanyCodeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CompanyCode)
                .HasConstraintName("FK__Products__Compan__3C69FB99");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shop__67C557C966617FF7");

            entity.ToTable("Shop");

            entity.Property(e => e.ShopId).ValueGeneratedNever();
            entity.Property(e => e.Paid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("paid");
            entity.Property(e => e.ShopNote).HasColumnType("text");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CustomerCodeNavigation).WithMany(p => p.Shops)
                .HasForeignKey(d => d.CustomerCode)
                .HasConstraintName("FK__Shop__CustomerCo__412EB0B6");
        });

        modelBuilder.Entity<ShopDetail>(entity =>
        {
            entity.HasKey(e => e.ShopDetailsId).HasName("PK__ShopDeta__A3F70B48E00F552C");

            entity.Property(e => e.ShopDetailsId).ValueGeneratedNever();

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.ShopDetails)
                .HasForeignKey(d => d.ProductCode)
                .HasConstraintName("FK__ShopDetai__Produ__44FF419A");

            entity.HasOne(d => d.ShopCodeNavigation).WithMany(p => p.ShopDetails)
                .HasForeignKey(d => d.ShopCode)
                .HasConstraintName("FK__ShopDetai__ShopC__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
