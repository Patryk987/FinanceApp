using System;
using System.Collections.Generic;
using FinanceApp.Entities;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
namespace FinanceApp.Entities;

public partial class FinanceAppContext : DbContext
{
    public FinanceAppContext()
    {
    }

    public FinanceAppContext(DbContextOptions<FinanceAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentPo> DocumentPos { get; set; }

    public virtual DbSet<FamilyGroup> FamilyGroups { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<Saving> Savings { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=ADRIAN\\SQLEXPRESS;Initial Catalog=FinanceApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasIndex(e => e.UserId, "FK_2");

            entity.HasIndex(e => e.IdShop, "FK_3");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.DataDokumentu).HasColumnType("date");
            entity.Property(e => e.Desc).HasMaxLength(100);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.IdShopNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.IdShop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdShop");

            entity.HasOne(d => d.User).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userID");
        });

        modelBuilder.Entity<DocumentPo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_asd");

            entity.HasIndex(e => e.IdDoc, "FK_2");

            entity.HasIndex(e => e.IdProd, "FK_3");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdDoc).HasColumnName("idDoc");
            entity.Property(e => e.IdProd).HasColumnName("idProd");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.IdDocNavigation).WithMany(p => p.DocumentPos)
                .HasForeignKey(d => d.IdDoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idDoc");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.DocumentPos)
                .HasForeignKey(d => d.IdProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idProd");
        });

        modelBuilder.Entity<FamilyGroup>(entity =>
        {
            entity.HasKey(e => e.IdGroup);

            entity.ToTable("FamilyGroup");

            entity.Property(e => e.IdGroup).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_payments");

            entity.HasIndex(e => e.UserId, "FK_2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AmountPln)
                .HasColumnType("money")
                .HasColumnName("amountPLN");
            entity.Property(e => e.AmountWal)
                .HasColumnType("money")
                .HasColumnName("amountWal");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TypeOfPayments).HasColumnName("typeOfPayments");
            entity.Property(e => e.Waluta)
                .HasMaxLength(3)
                .HasColumnName("waluta");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_UserId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.HasIndex(e => e.IdGroup, "FK_2");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .HasColumnName("barcode");
            entity.Property(e => e.IdGroup).HasColumnName("idGroup");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdGroup)
                .HasConstraintName("FK_idGroup");
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.HasKey(e => e.IdGroup);

            entity.ToTable("ProductGroup");

            entity.Property(e => e.IdGroup)
                .ValueGeneratedNever()
                .HasColumnName("idGroup");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Saving>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Oszczednosci");

            entity.HasIndex(e => e.Idpaymants, "FK_2");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdpaymantsNavigation).WithMany(p => p.Savings)
                .HasForeignKey(d => d.Idpaymants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Idpaymants");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.IdShop);

            entity.Property(e => e.IdShop).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
