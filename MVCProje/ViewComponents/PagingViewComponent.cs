using Microsoft.AspNetCore.Mvc;
using MVCProje.Models.DTO;

namespace MVCProje.ViewComponents
{
    public class PagingViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(HomeIndexViewModel model)
        {
            return View(model);
        }
    }
}
