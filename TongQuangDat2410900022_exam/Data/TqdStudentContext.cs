using Microsoft.EntityFrameworkCore;
using TongQuangDat2410900022_exam.Models;

namespace TongQuangDat2410900022_exam.Data;

public class TqdStudentContext(DbContextOptions<TqdStudentContext> options) : DbContext(options)
{
    public DbSet<TqdStudent> TqdStudents => Set<TqdStudent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TqdStudent>().ToTable("TqdStudent");
    }
}