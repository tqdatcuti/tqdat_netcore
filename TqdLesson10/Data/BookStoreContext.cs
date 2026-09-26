using Microsoft.EntityFrameworkCore;
using TqdLesson10.Models;

namespace TqdLesson10.Data;

public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Publisher> Publishers => Set<Publisher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(book => book.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Book>().HasOne(book => book.Category)
            .WithMany(category => category.Books)
            .HasForeignKey(book => book.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Book>().HasOne(book => book.Publisher)
            .WithMany(publisher => publisher.Books)
            .HasForeignKey(book => book.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}