using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Entities
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<DocumentPos> DocumentPos { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<FamilyGroup> FamilieGroup { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Savings> Savings { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DocumentPos>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(r => r.DataPos)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired();
                entity.HasOne<Documents>(d => d.Document)
                    .WithMany(p => p.DocPos)
                    .HasForeignKey(d => d.DocumentId);

                entity.HasOne(d => d.Products)
                    .WithMany(d => d.DocumentPos)
                    .HasForeignKey(p => p.ProductId);

            });

            modelBuilder.Entity<Savings>(entity =>
            {
                entity.HasIndex(e => e.Id)
                     .IsUnique();

                entity.Property(r => r.Name)
                     .IsRequired();
                entity.HasOne<Payments>(e=>e.Payments)
                    .WithOne(s => s.Savings)
                    .HasForeignKey<Savings>(s=>s.IdPayments);
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasIndex(r => r.Id)
                    .IsUnique();
                entity.Property(r => r.Name)
                    .IsRequired();
                entity.Property(e=>e.AmountPLN)
                    .IsRequired();
                entity.Property(k=>k.Category)
                    .IsRequired();
                entity.Property(k => k.AmountWal)
                    .IsRequired();
                entity.Property(w=>w.Currency)
                    .IsRequired();
                entity.HasOne<Users>(e => e.Users)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(s => s.UserId);

            });
        }

    }
}
