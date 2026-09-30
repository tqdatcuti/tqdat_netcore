using Microsoft.EntityFrameworkCore;
using TqdLesson12.Models;

namespace TqdLesson12.Data;

public class StudentManagerContext(DbContextOptions<StudentManagerContext> options) : DbContext(options)
{
    public DbSet<StudentClass> Classes => Set<StudentClass>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<StudentMark> Marks => Set<StudentMark>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentClass>().ToTable("StdClass");
        modelBuilder.Entity<Student>().ToTable("Student");
        modelBuilder.Entity<Subject>().ToTable("Subjects");
        modelBuilder.Entity<StudentMark>().ToTable("Marks");
        modelBuilder.Entity<StudentClass>().Property(item => item.ClassName).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Student>().Property(item => item.StudentAvatar).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Student>().HasOne(item => item.Class).WithMany(item => item.Students)
            .HasForeignKey(item => item.ClassId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Student>().HasIndex(item => item.StudentEmail).IsUnique();
        modelBuilder.Entity<Student>().HasIndex(item => item.StudentPhone).IsUnique();
        modelBuilder.Entity<Subject>().HasIndex(item => item.SubjectName).IsUnique();
        modelBuilder.Entity<Student>().Property(item => item.StudentBirthday).HasColumnType("date");
        modelBuilder.Entity<StudentMark>().HasKey(item => new { item.SubjectId, item.StudentId });
        modelBuilder.Entity<StudentMark>().Property(item => item.Score).HasColumnType("float");
    }
}