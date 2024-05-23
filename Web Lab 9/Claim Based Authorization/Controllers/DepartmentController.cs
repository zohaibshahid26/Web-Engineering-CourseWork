using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    public class DepartmentController : Controller
    {
        [Authorize(Policy = "RequireDepartmentCS")]
        public IActionResult CS()
        {
            return View();
        }

        [Authorize(Policy = "RequireDepartmentSE")]
        public IActionResult SE()
        {
            return View();
        }

        [Authorize(Policy = "RequireDepartmentIT")]
        public IActionResult IT()
        {
            return View();
        }

    }
}
