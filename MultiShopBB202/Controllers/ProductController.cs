using Microsoft.AspNetCore.Mvc;

namespace MultiShopBB202.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
