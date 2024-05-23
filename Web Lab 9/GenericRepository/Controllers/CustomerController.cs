using Microsoft.AspNetCore.Mvc;
using Tassk2.Models;

namespace Tassk2.Controllers
{
    public class CustomerController : Controller
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
        public IActionResult Add(Customer customer)
        {
            var genericRepository = new GenericRepository<Customer>(connectionString);
            genericRepository.Add(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var genericRepository = new GenericRepository<Customer>(connectionString);
            genericRepository.DeleteById(id);
            return RedirectToAction("Index", "Customer");
        }

        public IActionResult Details(int id)
        {
            var genericRepository = new GenericRepository<Customer>(connectionString);
            var customer = genericRepository.FindById(id);
            return RedirectToAction("Index");
        }
    }
}
