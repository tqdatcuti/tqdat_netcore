using TqdLesson12.Models;

namespace TqdLesson12.ViewModels;

public class CatalogHomeViewModel
{
    public List<Banner> Banners { get; set; } = [];
    public List<Product> Products { get; set; } = [];
}