using Microsoft.AspNetCore.Mvc;
using Tassk2.Models;

namespace Tassk2.Controllers
{
    public class ProductController : Controller
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Lab9;Integrated Security=True;";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            var genericRepository = new GenericRepository<Product>(connectionString);
            genericRepository.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var genericRepository = new GenericRepository<Product>(connectionString);
            genericRepository.DeleteById(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var genericRepository = new GenericRepository<Product>(connectionString);
            var customer = genericRepository.FindById(id);
            return RedirectToAction("Index");
        }
    }
}

