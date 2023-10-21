using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.OrdersCount = _context.Orders.Count();
            ViewBag.SealedCount = _context.OrderDetails.Count();
            
            ViewBag.ProductsBrands = _context.Products.Select(b=>b.ProductBrandId).Distinct().Count();
            ViewBag.OrdersUsers = _context.Orders.Select(u=>u.UserID).Distinct().Count();
            ViewBag.SealedOrders = _context.OrderDetails.Select(o=>o.OrderId).Distinct().Count();
            if (User.IsInRole("Admin"))
                return View();

            return RedirectToAction(nameof(Index), "Product");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}