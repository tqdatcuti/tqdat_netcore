namespace TqdLesson6.ViewModels;

public sealed record ProductViewModel(string Name, decimal Price, string ImageUrl);

public static class ProductCatalog
{
    private const string CookerImage = "https://pngimg.com/uploads/rice_cooker/rice_cooker_PNG19.png";

    public static IReadOnlyList<ProductViewModel> GetLatestProducts() =>
    [
        new("Nồi cơm điện cao tần Nagakawa NAG0102", 1890000, CookerImage),
        new("Nồi cơm điện cao tần Nagakawa NAG0102", 1890000, CookerImage),
        new("Nồi cơm điện cao tần Nagakawa NAG0102", 1890000, CookerImage)
    ];

    public static IReadOnlyList<ProductViewModel> GetHotProducts() =>
    [
        new("Nồi cơm điện cao tần Nagakawa NAG0102", 1890000, CookerImage),
        new("Nồi cơm điện cao tần Nagakawa NAG0102", 1890000, CookerImage),
        new("Nồi cơm điện cao tần Nagakawa NAG0102", 1890000, CookerImage)
    ];
}
