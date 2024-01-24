using Microsoft.AspNetCore.Mvc;
using MVCCrud.Models;
using System.Diagnostics;

namespace MVCCrud.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(StudentContext studentContext)
        {
            this.studentContext = studentContext;
        }
        private readonly StudentContext studentContext;

  
        public IActionResult Index()
        {
            
            return View(studentContext.Get());
        }

        
    }
}
