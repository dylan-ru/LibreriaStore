//Intall package: Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.EntityFrameworkCore.Tools

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AppStore.Models.Domain;

public class DatabaseContext : IdentityDbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>().HasMany(b => b.Category_Relation)
            .WithMany(cb => cb.Books_Relation)
            .UsingEntity<CategoryBook>(
                j => j.HasOne(cb => cb.Category).WithMany(c => c.CategoryBooks_Relation).HasForeignKey(cb => cb.CategoryId),
                j => j.HasOne(cb => cb.Book).WithMany(b => b.CategoryBooks_Relation).HasForeignKey(cb => cb.BookId),
                j => j.HasKey(cb => new { cb.CategoryId, cb.BookId })
            );
      
    }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<CategoryBook>? CategoryBooks { get; set; }
        //public DbSet<ApplicationUser>? ApplicationUsers { get; set; }


    
}