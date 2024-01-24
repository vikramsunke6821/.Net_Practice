using DapperDemo.Contracts;
using DapperDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        // Set the employee repository in the constructor of the EmployeeController
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepo.GetEmployees();
                if (employees.Any())
                    return Ok(employees);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error in GetEmployees: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); // You can customize the error response as needed
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            try
            {
                // Call the repository method to get the employee by Id
                var employee = await _employeeRepo.GetEmployeeById(id);

                if (employee != null)
                    return Ok(employee);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error in GetEmployeeById: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); // You can customize the error response as needed
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            try
            {
                // Validate the 'employee' parameter as needed

                await _employeeRepo.PostEmployee(employee);

                return Ok(employee);
                // Created status without a specific response body
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PostEmployee: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            try
            {
                var existingEmployee = await _employeeRepo.GetEmployeeById(id);

                if (existingEmployee == null)
                {
                    return NotFound(); // Return 404 Not Found if the resource doesn't exist
                }

                existingEmployee.Name = employee.Name;
                existingEmployee.Phone_Number = employee.Phone_Number;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.Department = employee.Department;

                // Call the repository method to update the employee
                await _employeeRepo.UpdateEmployee(existingEmployee);

                return Ok(existingEmployee); // Return 200 OK with the updated employee
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PutEmployee: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Controller Method (EmployeeController.cs)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                // Check if the employee with the specified id exists
                var existingEmployee = await _employeeRepo.GetEmployeeById(id);

                if (existingEmployee == null)
                {
                    return NotFound(); // Return 404 Not Found if the resource doesn't exist
                }

                // Call the repository method to delete the employee by Id
                await _employeeRepo.DeleteEmployeeById(id);

                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteEmployee: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

