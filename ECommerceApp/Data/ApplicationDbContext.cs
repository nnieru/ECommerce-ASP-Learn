using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Data;

public class ApplicationDbContext: DbContext 
{

    // constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    // table declarations
    public DbSet<Category> Categories { get; set; }
    
    // seeding - add initial data when table created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category {Id = 1, Name = "Action", DisplayOrder = 1},
            new Category {Id = 2, Name = "SciFi", DisplayOrder = 2},
            new Category {Id = 3, Name = "History", DisplayOrder = 3}
            );
    }
}