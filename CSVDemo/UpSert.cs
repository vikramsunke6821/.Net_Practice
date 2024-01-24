using CSVDemo.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace CSVDemo
{
    public class UpSert
    {
        public void UpsertCSV()
        {

            string filePath = @"C:\Users\QualMinds Admin\Desktop\CSVDemo1\Employees_List\employees.csv";
            bool isFirstRow = true;
            List<EmployeeModel> employeeList = [];

            try
            {
                using (StreamReader reader = new(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirstRow)
                        {
                            isFirstRow = false;
                            continue;
                        }

                        string[] values = line.Split(',');

                        EmployeeModel employee = new()
                        {
                            Employee_Id = (int)decimal.Parse(values[0]),
                            Name = values[1],
                            Phone_Number = values[4],
                            Salary = (int)decimal.Parse(values[7]),
                            Department = values[10]
                        };

                        employeeList.Add(employee);
                    }

                    if (employeeList != null && employeeList.Count > 0)
                    {
                        DataTable employeeDataTable = ConvertToDataTable(employeeList);
                        UpsertEmployees(employeeDataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Console.ReadKey();
        }



        public void UpsertXML()
        {
            string filePathXML = @"C:\Users\QualMinds Admin\Downloads\XMLFile\Employee\EmployeeXML\employee.xml";
            List<EmployeeModel> employeeList = [];

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(filePathXML);
                XmlNodeList nodesList = xmlDoc.SelectNodes("employees/employee");  // selects the list of xml nodes

                foreach (XmlNode node in nodesList)  // this will iterate each node from the nodeslist
                {
                    EmployeeModel employee = new EmployeeModel();
                    if (node.Attributes != null && node.Attributes["emp_id"] != null)
                    {
                        employee.Employee_Id = int.Parse(node.Attributes["emp_id"].Value);
                    }

                    employee.Name = node["name"]?.InnerText;
                    employee.Phone_Number = node["phone_number"]?.InnerText;
                    employee.Salary = Convert.ToInt32(node["salary"]?.InnerText);
                    employee.Department = node["department"]?.InnerText;

                    //Post(employee);
                    employeeList.Add(employee);
                }
                if (employeeList != null && employeeList.Count > 0)
                {
                    DataTable employeeDataTable = ConvertToDataTable(employeeList);
                    UpsertEmployees(employeeDataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpsertJSON()
        {
            string filePath = @"C:\Users\QualMinds Admin\Desktop\JsonDemo\JsonFile\JsonFile1\employees.json";

            try
            {
                string json = File.ReadAllText(filePath);
                List<EmployeeModel> employeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(json);

                if (employeeList != null && employeeList.Count > 0)
                {
                    DataTable employeeDataTable = ConvertToDataTable(employeeList);
                    UpsertEmployees(employeeDataTable);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private DataTable ConvertToDataTable(List<EmployeeModel> employeeList)
        {
            DataTable dataTable = new();

            dataTable.Columns.Add("Employee_Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Phone_Number", typeof(string));
            dataTable.Columns.Add("Salary", typeof(int));
            dataTable.Columns.Add("Department", typeof(string));

            foreach (var employee in employeeList)
            {
                dataTable.Rows.Add(
                    employee.Employee_Id,
                    employee.Name,
                    employee.Phone_Number,
                    employee.Salary,
                    employee.Department
                );
            }

            return dataTable;
        }

        private void UpsertEmployees(DataTable employeeDataTable)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlConnection con = new(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new("UpdateAndInsertEmployees", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@EmployeeData", employeeDataTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.EmployeeTableType";

                    using (SqlDataAdapter adapter = new(cmd))
                    {
                        DataSet dataSet = new();
                        adapter.Fill(dataSet);
                    }
                }
            }
        }

        
    }
}


/*private static void Post(EmployeeModel model)
{
    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("PostEmployeesList", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@emp_id", model.Employee_Id));
        cmd.Parameters.Add(new SqlParameter("@name", model.Name));
        cmd.Parameters.Add(new SqlParameter("@phone_number", model.Phone_Number));
        cmd.Parameters.Add(new SqlParameter("@salary", model.Salary));
        cmd.Parameters.Add(new SqlParameter("@department", model.Department));

        cmd.ExecuteNonQuery();
    }
}*/
