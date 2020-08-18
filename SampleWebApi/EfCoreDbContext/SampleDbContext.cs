﻿using Microsoft.EntityFrameworkCore;
using SampleWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.EfCoreDbContext
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItems>()
                .HasOne(p => p.Order)
                .WithMany(b => b.OrderItems)
                .HasForeignKey(c => c.OrderId);
        }
    }
}
