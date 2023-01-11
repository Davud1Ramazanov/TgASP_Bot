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

        public virtual DbSet<Gadget> Gadgets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-3H1AEF60\\SQLEXPRESS;Initial Catalog=gadgetstore_db;Integrated security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gadget>(entity =>
            {
                entity.ToTable("Gadget");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
