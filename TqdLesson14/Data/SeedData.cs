using TqdLesson14.Models;

namespace TqdLesson14.Data;

public static class SeedData
{
    public static void Initialize(BookStoreContext context)
    {
        if (context.Books.Any())
        {
            return;
        }

        var categories = new[]
        {
            new Category { CategoryName = "Ngôn ngữ lập trình" },
            new Category { CategoryName = "Công nghệ phần mềm" },
            new Category { CategoryName = "Kỹ năng nghề nghiệp" }
        };
        var publishers = new[]
        {
            new Publisher { PublisherName = "NXB Giáo dục", Phone = "0901234567" },
            new Publisher { PublisherName = "NXB Trẻ", Phone = "02839316289" }
        };
        context.Categories.AddRange(categories);
        context.Publishers.AddRange(publishers);
        context.SaveChanges();

        context.Books.AddRange(
            new Book { Title = "C# và ASP.NET Core từ đầu", Author = "Nguyễn Minh Anh", Price = 180000, ReleaseYear = 2025, CategoryId = categories[0].CategoryId, PublisherId = publishers[0].PublisherId, Description = "Sách hướng dẫn cơ bản về lập trình C# và xây dựng ứng dụng web trên ASP.NET Core." },
            new Book { Title = "Entity Framework Core thực hành", Author = "Trần Hương Ly", Price = 220000, ReleaseYear = 2025, CategoryId = categories[1].CategoryId, PublisherId = publishers[0].PublisherId, Description = "Hướng dẫn thực hành truy vấn và quản lý dữ liệu với Entity Framework Core." },
            new Book { Title = "Lập trình hướng đối tượng hiệu quả", Author = "Lê Quốc Bảo", Price = 150000, ReleaseYear = 2024, CategoryId = categories[2].CategoryId, PublisherId = publishers[1].PublisherId, Description = "Sách tham khảo kỹ năng lập trình và tư duy thiết kế phần mềm." });
        context.SaveChanges();
    }
}