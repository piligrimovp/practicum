using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace lab2
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Client>  Clients  { get; set; }
        public DbSet<Order>   Orders   { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Client)
                .WithMany(e => e.Orders)
                .HasForeignKey(p => p.СlientId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Service)
                .WithMany(e => e.Orders)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
