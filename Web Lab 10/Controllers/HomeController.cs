using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Lab_10.Models;

namespace Web_Lab_10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(Request.Cookies.ContainsKey("Login") && Request.Cookies["Login"] == "True")
            {
                var Color = Request.Cookies["Color"];
                ViewBag.Color = Color;
                Product product1 = new Product { ProductId = 1, Name = "Product 1", Description = "Description 1", Price = 100 };
                Product product2 = new Product { ProductId = 2, Name = "Product 2", Description = "Description 2", Price = 200 };
                Product product3 = new Product { ProductId = 3, Name = "Product 3", Description = "Description 3", Price = 300 };
                Order order1 = new Order { OrderId = 1, CreatedAt = DateTime.Now, Amount = 0 };
                Order order2 = new Order { OrderId = 2, CreatedAt = DateTime.Now, Amount = 0 };
                Order order3 = new Order { OrderId = 3, CreatedAt = DateTime.Now, Amount = 0 };
                List<OrderProductViewModel> orders= new List<OrderProductViewModel>();
                orders.Add(new OrderProductViewModel { Order = order1, Products = new List<Product> { product1, product2 } });
                orders.Add(new OrderProductViewModel { Order = order2, Products = new List<Product> { product2, product3 } });
                orders.Add(new OrderProductViewModel { Order = order3, Products = new List<Product> { product1, product3,product1 } });
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
           
        }

        public IActionResult Privacy()
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
