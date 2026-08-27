using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class BarberSoftDbContext : DbContext
    {
        public BarberSoftDbContext(DbContextOptions<BarberSoftDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Appointment>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne<Service>()
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // SysAdmin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Sysadmin",
                    Email = "sysadmin@example.com",
                    Password = "Sysadmin123",
                    Role = UserRole.SysAdmin
                },
                new User
                {
                    Id = 2,
                    Name = "Barber",
                    Email = "barber@example.com",
                    Password = "Barber@12345",
                    Role = UserRole.Barber
                },
                new User
                {
                    Id = 3,
                    Name = "Admin",
                    Email = "admin@example.com",
                    Password = "Admin@12345",
                    Role = UserRole.Admin
                }
            );

            // Sample clients
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Name = "Juan García",
                    Phone = "+5491112345678"
                },
                new Client
                {
                    Id = 2,
                    Name = "Carlos López",
                    Phone = "+5491123456789"
                },
                new Client
                {
                    Id = 3,
                    Name = "Miguel Rodríguez",
                    Phone = "+5491134567890"
                }
            );

            // Sample services
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Corte Clásico",
                    Price = 150.00m,
                    DurationInMinutes = 30,
                    IsActive = true
                },
                new Service
                {
                    Id = 2,
                    Name = "Corte Premium",
                    Price = 250.00m,
                    DurationInMinutes = 45,
                    IsActive = true
                },
                new Service
                {
                    Id = 3,
                    Name = "Afeitada",
                    Price = 100.00m,
                    DurationInMinutes = 20,
                    IsActive = true
                },
                new Service
                {
                    Id = 4,
                    Name = "Tinte de Barba",
                    Price = 120.00m,
                    DurationInMinutes = 25,
                    IsActive = true
                }
            );
        }
    }
}