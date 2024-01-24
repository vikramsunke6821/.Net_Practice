namespace CSVDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Select below methods as per your requirment:  \n 1.CSV \n 2.XML \n 3.JSON");
            int n=Convert.ToInt32(Console.ReadLine());
            UpSert upSert = new();
            if (n == 1)
                upSert.UpsertCSV();

            else if (n == 2)
                upSert.UpsertXML();

            else if (n == 3)
                upSert.UpsertJSON();
            
            else
                Console.WriteLine("Please enter either 1 or 2 or 3");
             
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
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@emp_id", model.Employee_Id));
                cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                cmd.Parameters.Add(new SqlParameter("@phone_number", model.Phone_Number));
                cmd.Parameters.Add(new SqlParameter("@salary", model.Salary));
                cmd.Parameters.Add(new SqlParameter("@department", model.Department));

                cmd.ExecuteNonQuery();
            }
        }*/
    
