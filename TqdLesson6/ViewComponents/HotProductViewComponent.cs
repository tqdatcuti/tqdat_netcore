using TqdLesson6.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TqdLesson6.ViewComponents;

public sealed class HotProductViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(ProductCatalog.GetHotProducts());
    }
}
