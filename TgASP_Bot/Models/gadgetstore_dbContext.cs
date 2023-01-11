using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TgASP_Bot.Models
{
    public partial class gadgetstore_dbContext : DbContext
    {
        public gadgetstore_dbContext()
        {
        }

        public gadgetstore_dbContext(DbContextOptions<gadgetstore_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Gadget> Gadgets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-3H1AEF60\\SQLEXPRESS;Database=gadgetstore_db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameGadgets)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME_GADGETS");
            });

            modelBuilder.Entity<Gadget>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Gadgets)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK__Gadgets__ID_Cate__72C60C4A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
