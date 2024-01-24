using Newtonsoft.Json;

namespace CSVDemo.Models
{
    public class EmployeeModel
    {
        [JsonProperty("emp_id")]
        public int?    Employee_Id  { get; set; }
        public string? Name         { get; set; }
        public string? Phone_Number { get; set; }
        public int?    Salary       { get; set; }
        public string? Department   { get; set; }
    }
}

