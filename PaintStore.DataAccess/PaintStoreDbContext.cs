using System;
using Microsoft.EntityFrameworkCore;
using PaintStore.Models;

namespace PaintStore.DataAccess;

public class PaintStoreDbContext : DbContext
{
    public DbSet<PaintProduct> PaintProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }

    public PaintStoreDbContext(DbContextOptions<PaintStoreDbContext> options):base(options)
    {      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaintProduct>().HasKey(p=>p.Id);
        modelBuilder.Entity<PaintProduct>().Property(p=>p.Price).IsRequired();
        modelBuilder.Entity<PaintProduct>().Property(p=>p.Name).IsRequired().HasMaxLength(50);
        base.OnModelCreating(modelBuilder);
    }
}
