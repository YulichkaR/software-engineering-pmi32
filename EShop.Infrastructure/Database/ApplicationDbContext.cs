using System.Data.Common;
using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Item> Items { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
}