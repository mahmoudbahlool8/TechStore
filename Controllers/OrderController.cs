using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TechStore.Data;
using TechStore.Models;
using TechStore.Utility;

namespace TechStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public OrderController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: Order
        public async Task<IActionResult> Order()
        {
            if (!User.IsInRole("User"))
                return LocalRedirect("/Identity/Account/Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(Order order)
        {
            if (ModelState.IsValid)
            {
                List<Product> products = HttpContext.Session.Get<List<Product>>("Products");

                if (products != null)
                {
                    foreach (var product in products)
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.ProductId = product.Id;
                        order.OrderDetails.Add(orderDetails);
                        order.orderDetails = orderDetails.OrderId;
                    }
                }
                _context.Add(order);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Order submitted successfully");
                HttpContext.Session.Remove("Products");
                return RedirectToAction(nameof(Index), "Product");
            }

            return View();
        }

    }
}
