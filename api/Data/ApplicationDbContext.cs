using System;
using System.Reflection.Metadata;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<HomeAddress> HomeAddresses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemEvent> ItemEvents { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<UsState> UsStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HomeAddress>()
                .HasOne(e => e.UsState)
                .WithMany()
                .HasForeignKey(e => e.UsStateId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Item>()
                .HasOne(e => e.ItemType)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.ItemTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Item>()
                .HasOne(e => e.AssignedTo)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemEvent>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.ItemEvents)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemEvent>()
                .HasOne(e => e.Item)
                .WithMany(e => e.ItemEvents)
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemEvent>()
                .HasOne(e => e.EventType)
                .WithMany(e => e.ItemEvents)
                .HasForeignKey(e => e.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
