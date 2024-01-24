using Dapper;
using DapperDemo.Context;
using DapperDemo.Contracts;
using DapperDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DapperDemo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeRepository(EmployeeDBContext context, IConfiguration _configuration)
        {
            _context = context;
            _connectionString = _configuration.GetConnectionString("conStr");
        }



        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var sqlQuery = "SELECT * FROM EmployeeCSV..EmployeeTable";
                using (SqlConnection connection = new(_connectionString))
                {
                    connection.Open();
                    var employees = await connection.QueryAsync<Employee>(sqlQuery);
                    return employees;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error in GetEmployees: {ex.Message}");
                throw; // Rethrow the exception or handle as appropriate for your application
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                var sqlQuery = "SELECT * FROM EmployeeCSV..EmployeeTable WHERE Emp_Id = @Id";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var employee = await connection.QueryFirstOrDefaultAsync<Employee>(sqlQuery, new { id });

                    return employee;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetEmployeeById: {ex.Message}");

                throw;
            }
        }
        // Repository method responsible for adding a new employee to the database
        public async Task PostEmployee(Employee employee)
        {
            try
            {

                var sqlQuery = "INSERT INTO EmployeeCSV..EmployeeTable (Emp_Id, Name, Phone_Number, Salary, Department) " +
                                "VALUES (@Emp_Id, @Name, @Phone_Number, @Salary, @Department);";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Execute the SQL query asynchronously, passing parameters from the 'employee' object
                    await connection.ExecuteAsync(sqlQuery, new
                    {
                        employee.Emp_Id,
                        employee.Name,
                        employee.Phone_Number,
                        employee.Salary,
                        employee.Department
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in PostEmployee: {ex.Message}");

                // Rethrow the exception for higher-level handling
                throw;
            }
        }


        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                var sqlQuery = "UPDATE EmployeeCSV..EmployeeTable " +
                               "SET Name = @Name, " +
                               "Phone_Number = @PhoneNumber, " +
                               "Salary = @Salary, " +
                               "Department = @Department " +
                               "WHERE Emp_Id = @Emp_Id;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Execute the SQL query asynchronously, passing parameters from the 'employee' object
                    await connection.ExecuteAsync(sqlQuery, new
                    {
                        employee.Emp_Id,
                        employee.Name,
                        employee.Phone_Number,
                        employee.Salary,
                        employee.Department
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateEmployee: {ex.Message}");
                throw;
            }
        }

        // Repository Method (EmployeeRepository.cs)
        public async Task DeleteEmployeeById(int id)
        {
            try
            {
                var sqlQuery = "DELETE FROM EmployeeCSV..EmployeeTable WHERE Emp_Id = @Id";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Execute the SQL query asynchronously, passing parameters from the 'id' parameter
                    await connection.ExecuteAsync(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteEmployeeById: {ex.Message}");
                throw;
            }
        }

    }
}
