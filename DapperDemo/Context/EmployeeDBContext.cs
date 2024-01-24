using Microsoft.Extensions.Configuration;

namespace DapperDemo.Context
{
    public class EmployeeDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("conStr");
        }
    }
}
