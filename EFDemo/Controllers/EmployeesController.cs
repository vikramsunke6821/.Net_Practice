using EFDemo.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using EFDemo.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EFDemo.Data;
using System.Linq;

namespace EFDemo.Controllers
{
    public class EmployeesController : Controller

    {
        private readonly MVCDemo dbContext;
        public EmployeesController(MVCDemo dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var emp = await dbContext.Employees.ToListAsync();
            return View(emp);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel viewModel)
        {
            var emp = new Employee()
            {
                Name  = viewModel.Name,
                Email = viewModel.Email,
                Age   = viewModel.Age,
                Number= viewModel.Number
            };
            dbContext.Employees.Add(emp);
            dbContext.SaveChanges();
            return RedirectToAction("List", "Employees");
        }


       


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emp = dbContext.Employees.Find(id);
            
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(AddEmployeeViewModel viewModel)
        {

            var emp = dbContext.Employees.Find(viewModel.Id);
            if(emp is not null)
            {
                emp.Name = viewModel.Name;
                emp.Email = viewModel.Email;
                emp.Age = viewModel.Age;
                emp.Number = viewModel.Number;
                dbContext.Employees.Update(emp);
                dbContext.SaveChanges();
            }
            return RedirectToAction("List","Employees");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emp=dbContext.Employees.Find(id);
            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();
            return RedirectToAction("List", "Employees");
        }
     
    }
}
