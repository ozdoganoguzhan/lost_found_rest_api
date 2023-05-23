using Microsoft.EntityFrameworkCore;
using LostFound.Core.Entities;
using System.Net;

namespace LostFound.Infrastructure;

public class LFDbContext : DbContext
{
    
    public LFDbContext(DbContextOptions options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubCategory>()
            .HasOne<Category>()
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.categoryId);
    }

    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<SubCategory> SubCategories { get; set; } = null!;
}