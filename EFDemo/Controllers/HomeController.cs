using EFDemo.Models;

using EFDemo.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using Microsoft.EntityFrameworkCore;
namespace EFDemo.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(EmployeeService employeeContext)
        {
            this.employeeContext = employeeContext;
        }
        private readonly EmployeeService employeeContext;

        public IActionResult Index()
        {

            return View(employeeContext.Get());
        }
        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Post(AddEmployeeViewModel model)
        {
            AddEmployeeViewModel emp = new();
            emp.Name = model.Name;
            emp.Email = model.Email;
            emp.Age = model.Age;
            emp.Number = model.Number;
            employeeContext.Post(emp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Put(int Id)
        {

            return View(employeeContext.GetById(Id));
        }
       
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}
