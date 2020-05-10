using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Data
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<lab5.Models.Client> Client { get; set; }

        public DbSet<lab5.Models.Service> Service { get; set; }

        public DbSet<lab5.Models.Order> Orders { get; set; }
        public DbSet<lab5.Models.OrderServices> OrderServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Order>()
                .HasOne(e => e.Client)
                .WithMany(e => e.Orders)
                .HasForeignKey(p => p.СlientId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderServices>()
                .HasOne(e => e.Order)
                .WithMany(e => e.ServiceOrder)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderServices>()
                .HasOne(e => e.Service)
                .WithMany(e => e.ServiceOrder)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<OrderServices>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ServiceOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceOrder)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasMany(d => d.ServiceOrder)
                      .WithOne(p => p.Service)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasMany(d => d.Orders)
                      .WithOne(p => p.Client)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
