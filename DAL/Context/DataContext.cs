using DAL.Config;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusLay.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Insurance> Insuranses { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new ContractConfig());
            modelBuilder.ApplyConfiguration(new SessionConfig());
            modelBuilder.ApplyConfiguration(new InsuranceConfig());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
            }
        }

    }
}
