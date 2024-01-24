using DapperDemo.Model;

namespace DapperDemo.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task PostEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);

        Task DeleteEmployeeById(int id);



    }
}