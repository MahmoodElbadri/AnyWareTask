using AnyWareTask.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AnyWareTask.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>()
            .Property(tmp => tmp.Amount)
            .HasPrecision(18, 2);
            
        modelBuilder.Entity<Order>()
            .HasKey(tmp => tmp.OrderId);


        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                OrderId = Guid.Parse("d3e4f5a6-1b2c-3d4e-5f6a-7b8c9d0e1f2a"), 
                CustomerName = "Ahmed Ali",
                Product = "Brake Pads",
                Amount = 150.50m,
                CreatedAt = DateTime.Parse("2024-01-01") 
            },
            new Order
            {
                OrderId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                CustomerName = "Sarah Samy",
                Product = "Oil Filter",
                Amount = 75.00m,
                CreatedAt = DateTime.Parse("2024-01-02")
            }
        );

    }
}
