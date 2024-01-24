using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace MVCCrud.Models
{

    

    public class StudentContext
    {
        private readonly IConfiguration _configuration;

        public StudentContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<StudentModel> Get()
        {
            


        List<StudentModel> listofmodel = new List<Models.StudentModel>();
            string connectionString = _configuration.GetConnectionString("constr");
            //String connectionString = "Data Source=QUAL-LT87PXQL3\\SQLEXPRESS;Initial Catalog=StudentDetails;Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new ("GetstudentRecords", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentModel stu = new();
                    stu.Id = (reader["id"] == DBNull.Value ? default(System.Int32) : Convert.ToInt32(reader["id"]));
                    stu.Name = (reader["name"] == DBNull.Value ? default(System.String) : Convert.ToString(reader["name"]));
                    stu.Email = (reader["email"] == DBNull.Value ? default(System.String) : Convert.ToString(reader["email"]));
                    stu.Age = (reader["age"] == DBNull.Value ? default(System.Int32) : Convert.ToInt32(reader["age"]));

                    //DBNull.Value -> this represents NULL value from DB
                    // NULL => this represents NULL value of C#

                    stu.Number = (reader["number"] == DBNull.Value? default(System.String): Convert.ToString(reader["number"]));

                    listofmodel.Add(stu);

                }
                return listofmodel;


            }
        }


    }
}
