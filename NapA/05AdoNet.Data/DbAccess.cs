using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05AdoNet.Data
{
    public class DbAccess
    {
        private const string connectionString = "Server=.\\sqlexpress;Database=SchoolContext0;Trusted_Connection=True;";

        public List<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>();

            var ds = new DataSet();
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("select Id, FirstName, LastName, ClassCode from Teachers", con))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "Teachers");
                    }
                }
            }

            foreach (DataRow row in ds.Tables["Teachers"].Rows)
            {
                var teacher = new Teacher()
                {
                    Id = row.Field<int>("Id")
                    ,FirstName = row.Field<string>("FirstName")
                    ,LastName = row.Field<string>("LastName")
                    ,ClassCode = row.Field<string>("ClassCode")
                };
                teachers.Add(teacher);
            }

            return teachers;
        }
    }
}
