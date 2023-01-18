using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Models;
using System.Diagnostics;

namespace MultiShopBB202.Controllers
{
    public class HomeController : Controller
    {

        
        public IActionResult Index()
        {
            return View();
        }

    }
}