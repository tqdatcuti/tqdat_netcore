using TqdLesson10.Models;

namespace TqdLesson10.Data;

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
            new Category { CategoryName = "Lập trình" },
            new Category { CategoryName = "Cơ sở dữ liệu" },
            new Category { CategoryName = "Kỹ năng" }
        };
        var publishers = new[]
        {
            new Publisher { PublisherName = "DEV Master", Phone = "0901234567" },
            new Publisher { PublisherName = "Nhà xuất bản Trẻ", Phone = "02839316289" }
        };
        context.Categories.AddRange(categories);
        context.Publishers.AddRange(publishers);
        context.SaveChanges();

        context.Books.AddRange(
            new Book { Title = "Sách mẫu 01", Author = "Tác giả mẫu 01", Price = 180000, ReleaseYear = 2025, CategoryId = categories[0].CategoryId, PublisherId = publishers[0].PublisherId, Description = "Dữ liệu minh họa cho thao tác CRUD." },
            new Book { Title = "Sách mẫu 02", Author = "Tác giả mẫu 02", Price = 220000, ReleaseYear = 2025, CategoryId = categories[1].CategoryId, PublisherId = publishers[0].PublisherId, Description = "Dữ liệu minh họa cho truy vấn EF Core." },
            new Book { Title = "Sách mẫu 03", Author = "Tác giả mẫu 03", Price = 150000, ReleaseYear = 2024, CategoryId = categories[2].CategoryId, PublisherId = publishers[1].PublisherId, Description = "Dữ liệu mẫu trong database." });
        context.SaveChanges();
    }
}