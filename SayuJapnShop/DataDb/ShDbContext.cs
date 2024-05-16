using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SayuJapnShop.Models;

namespace SayuJapnShop.DataDb
{
    public partial class ShDbContext : DbContext
    {
        public ShDbContext()
        {
        }

        public ShDbContext(DbContextOptions<ShDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Export> Exports { get; set; }


        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Vsale> VSales { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Vpurchase> Vpurchases { get; set; }

        public virtual DbSet<Vinvoice> Vinvoices { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Import> Imports { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vexport> Vexports { get; set; }
        public virtual DbSet<VImport> VImports { get; set; }
        public virtual DbSet<Vitem> Vitems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.CategoryName).HasMaxLength(50);
                entity.Property(e => e.Descriptions).HasMaxLength(50);
                entity.Property(e => e.status).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.Address).HasMaxLength(50);
                entity.Property(e => e.CustomerName).HasMaxLength(50);
                entity.Property(e => e.Gender).HasMaxLength(50);
                entity.Property(e => e.status).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Export>(entity =>
            {
                entity.ToTable("Exports");

                entity.Property(e => e.ExportId).HasColumnName("ExportID");
                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
                entity.Property(e => e.Amount).HasColumnName("Amount");
                entity.Property(e => e.Qty).HasColumnName("Qty");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.ItemID).HasColumnName("ItemID");

                entity.HasOne(d => d.Item).WithMany(p => p.Exports)
                    .HasForeignKey(d => d.ItemID)
                    .IsRequired();
                entity.HasOne(d => d.Invoice).WithMany(p => p.Exports)
                    .HasForeignKey(d => d.InvoiceId)
                    .IsRequired();
            });

            modelBuilder.Entity<Import>(entity =>
            {
                entity.ToTable("Imports");

                entity.Property(e => e.ImportId).HasColumnName("ImportID");
                entity.Property(e => e.SupplierID).HasColumnName("SupplierID");
                entity.Property(e => e.ItemID).HasColumnName("ItemID");
                entity.Property(e => e.Qty).HasColumnName("Qty");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Cost).HasColumnName("Cost");
                entity.Property(e => e.Remains).HasColumnName("Remains");
                entity.Property(e => e.Date).HasMaxLength(50).IsUnicode(false);
                entity.HasOne(d => d.Item).WithMany(p => p.Imports)
                    .HasForeignKey(d => d.ItemID)
                    .IsRequired();

                entity.HasOne(d => d.Supplier).WithMany(p => p.Imports)
                    .HasForeignKey(d => d.SupplierID)
                    .IsRequired();
            });


            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");
                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item).WithOne(p => p.Purchases)
                    .HasForeignKey<Purchase>(d => d.ItemId)
                    .IsRequired();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoices");

                entity.Property(e => e.InvoiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("InvoiceID");

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.CustomerID)
                    .HasColumnName("CustomerID");
                entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerID)
                    .IsRequired();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId);
                entity.Property(e => e.ItemId).HasColumnName("ItemID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.status).HasMaxLength(50).IsUnicode(false);
                entity.HasOne(d => d.Category).WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .IsRequired();

                entity.HasOne(d => d.Purchases).WithOne(p => p.Item)
                    .HasForeignKey<Purchase>(t=>t.ItemId);

                entity.HasOne(d => d.Sales).WithOne(e => e.Item)
                                .HasForeignKey<Sale>(t => t.ItemId);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
                entity.Property(e => e.Address).HasMaxLength(50);
                entity.Property(e => e.CompanyName).HasMaxLength(50);
                entity.Property(e => e.Owner).HasMaxLength(50);
                entity.Property(e => e.status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.ItemId);
                entity.Property(e => e.ItemId).HasColumnName("ItemID");
                entity.HasOne(d => d.Item).WithOne(e=>e.Sales)
                                .HasForeignKey<Sale>(t=>t.ItemId)
                                .IsRequired();
            });

            modelBuilder.Entity<Vpurchase>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VPurchase");
                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.ItemId).HasColumnName("ItemID");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<Vsale>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VSale");
                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.ItemId).HasColumnName("ItemID");
            });

            modelBuilder.Entity<Vitem>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VItem");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
                entity.Property(e => e.Descriptions).HasMaxLength(50);
                entity.Property(e => e.ItemId).HasColumnName("ItemID");
                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.status).HasMaxLength(50);
            });


            modelBuilder.Entity<VImport>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VImport");

                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.CompanyName).HasMaxLength(50);
                entity.Property(e => e.Qty).HasColumnName("Qty");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Remains).HasColumnName("Remains");
                entity.Property(e => e.Cost).HasColumnName("Cost");
                entity.Property(e => e.ImportId).HasColumnName("ImportID");
                entity.Property(e => e.Date).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<Vexport>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VExport");

                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.CustomerName).HasMaxLength(50);
                entity.Property(e => e.Qty).HasColumnName("Qty");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Amount).HasColumnName("Amount");
                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
                entity.Property(e => e.ExportId).HasColumnName("ExportID");
                entity.Property(e => e.Date).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<Vinvoice>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VInvoice");
                entity.HasKey(e => e.InvoiceId);
                entity.Property(e => e.CustomerName).HasColumnName("CustomerName");
                entity.Property(e => e.Date).HasColumnName("Date").IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
