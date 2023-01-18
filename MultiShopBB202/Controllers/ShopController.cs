using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShopBB202.Context;
using MultiShopBB202.Models;

namespace MultiShopBB202.Controllers
{
    public class ShopController : Controller
    {
        private readonly MultiDbContext _context;

        public ShopController(MultiDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(x => x.ProductImages).ToList();
            return View(products); 
        }

        public IActionResult Detail(int id)
        {
            Product? product = _context.Products.Include(x=>x.ProductImages).Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).FirstOrDefault(x=>x.Id==id);

            if(product == null) return NotFound();

            return View(product);
        }
    }
}
