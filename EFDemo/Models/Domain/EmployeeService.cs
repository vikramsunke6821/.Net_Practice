using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EFDemo.Models;

public class EmployeeService
{
    private readonly IConfiguration _configuration;

    public EmployeeService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public List<EmployeeModel> Get()
    {



        List<EmployeeModel> listofmodel = new List<Models.EmployeeModel>();
        string connectionString = _configuration.GetConnectionString("constr");

        using (SqlConnection con = new SqlConnection(connectionString))
        {

            string getlist = "select * from Employees";
            SqlCommand cmd = new(getlist, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                EmployeeModel emp = new();
                emp.Id = (reader["id"] == DBNull.Value ? default : Convert.ToInt32(reader["id"]));
                emp.Name = (reader["name"] == DBNull.Value ? default : Convert.ToString(reader["name"]));
                emp.Email = (reader["email"] == DBNull.Value ? default : Convert.ToString(reader["email"]));
                emp.Age = (reader["age"] == DBNull.Value ? default(System.Int32) : Convert.ToInt32(reader["age"]));
                emp.Number = (reader["number"] == DBNull.Value ? default : Convert.ToString(reader["number"]));

                //DBNull.Value -> this represents NULL value from DB
                // NULL => this represents NULL value of C#

                emp.Number = (reader["number"] == DBNull.Value ? default(System.String) : Convert.ToString(reader["number"]));

                listofmodel.Add(emp);

            }
            return listofmodel;


        }
    }

    public void Post(AddEmployeeViewModel model)
    {

        string connectionString = _configuration.GetConnectionString("constr");

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("PostEmployeeRecord", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
            cmd.Parameters.Add(new SqlParameter("@Email", model.Email));
            cmd.Parameters.Add(new SqlParameter("@Age", model.Age));
            cmd.Parameters.Add(new SqlParameter("@Number", model.Number));
            cmd.ExecuteNonQuery();
        }
    }



    public EmployeeModel GetById(int Id)
    {
        string connectionString = _configuration.GetConnectionString("constr");

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            SqlCommand cmd = new("GetEmployeeById", con);
            cmd.Parameters.AddWithValue("@Id", Id);


            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            EmployeeModel emp = new()
            {
                Id = (reader["id"] == DBNull.Value ? default : Convert.ToInt32(reader["id"])),
                Name = (reader["name"] == DBNull.Value ? default : Convert.ToString(reader["name"])),
                Email = (reader["email"] == DBNull.Value ? default : Convert.ToString(reader["email"])),
                Age = (reader["age"] == DBNull.Value ? default : Convert.ToInt32(reader["age"])),
                Number = (reader["number"] == DBNull.Value ? default : Convert.ToString(reader["number"]))
            };
            cmd.ExecuteNonQuery();


            return emp;

        }
    }




}
