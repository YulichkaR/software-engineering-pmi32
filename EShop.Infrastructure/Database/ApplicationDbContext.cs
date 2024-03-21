using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Models.ProductType>().HasData(
            new Domain.Models.ProductType {Id = Guid.NewGuid(), Name = "Electronics"},
            new Domain.Models.ProductType {Id = Guid.NewGuid(), Name = "Clothing"},
            new Domain.Models.ProductType {Id = Guid.NewGuid(), Name = "Books"}
        );
    }

    public DbSet<Domain.Models.Product> Items { get; set; }
    public DbSet<Domain.Models.ProductType> ProductTypes { get; set; }
    public DbSet<Domain.Models.User> Users { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Domain.Models.Order> Orders { get; set; }
}