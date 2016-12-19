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
                using (var cmd = new SqlCommand("select Id, FirstName, LastName, ClassCode, Subject_Id from Teachers", con))
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
                    ,Subject_Id = row.Field<int>("Subject_Id")
                };
                teachers.Add(teacher);
            }

            return teachers;
        }

        public object DeleteTeacher(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("DELETE FROM Teachers WHERE Id = @Id", con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    //lefuttatjuk a parancsunkat ami visszatér az érintett sorok számával
                    var affectedRows = cmd.ExecuteNonQuery();
                    return affectedRows;
                }
            }
        }

        public Teacher ReadTeacher(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT Id, FirstName, LastName, ClassCode, Subject_Id FROM Teachers WHERE Id = @Id", con))
                {
                    //Létrehozzuk a paramétert
                    var param = cmd.CreateParameter();
                    param.DbType = DbType.Int32;
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = "@Id";
                    param.Value = id;

                    //Hozzáadjuk a parancshoz
                    cmd.Parameters.Add(param);

                    //végrehajtjuk a parancsot és nyitunk egy reader-t
                    var reader = cmd.ExecuteReader();

                    //megpróbálunk ráállni az első sorra
                    if (!reader.Read())
                    { //ha nem sikerült, jelezzük
                        return null;
                    }

                    //Kiolvassuk az adatokat egy új példányba
                    var teacher = new Teacher()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id"))
                        ,FirstName = reader.GetString(reader.GetOrdinal("FirstName"))
                        ,LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        ,ClassCode = reader.GetString(reader.GetOrdinal("ClassCode"))
                        ,Subject_Id = reader.GetInt32(reader.GetOrdinal("Subject_Id"))
                    };

                    //és ezzel visszatérünk
                    return teacher;
                }
            }

        }

        public int UpdateTeacher(Teacher teacher)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("UPDATE Teachers SET FirstName=@FirstName, LastName=@LastName, ClassCode=@ClassCode, Subject_Id=@Subject_Id WHERE Id = @Id", con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.NVarChar, -1).Value = teacher.Id;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, -1).Value = teacher.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, -1).Value = teacher.LastName;
                    cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar, -1).Value = teacher.ClassCode;
                    cmd.Parameters.Add("@Subject_Id", SqlDbType.Int).Value = teacher.Subject_Id;

                    //lefuttatjuk a parancsunkat ami visszatér az érintett sorok számával
                    var affectedRows = cmd.ExecuteNonQuery();
                    return affectedRows;
                }
            }
        }

        public int CreateTeacher(Teacher teacher)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("INSERT Teachers (FirstName, LastName, ClassCode, Subject_Id) VALUES (@FirstName, @LastName, @ClassCode, @Subject_Id); SELECT SCOPE_IDENTITY() AS ID", con))
                {
                    //Mivel string-ből hozta létre a táblánkat a CodeFirst, ezért ez az adatbázisban NVarChar(max)
                    //típus lett. Ezt a 'max' értéket a paraméterekben a -1 jelöli.
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, -1).Value = teacher.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, -1).Value = teacher.LastName;
                    cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar, -1).Value = teacher.ClassCode;
                    cmd.Parameters.Add("@Subject_Id", SqlDbType.Int).Value = teacher.Subject_Id;

                    //Lefuttatjuk a parancsot és nyitunk egy reader-t az azonosító betöltéséhez.
                    var reader = cmd.ExecuteReader();
                    //ráállunk a következő sorra, ami az első sor lesz
                    if (!reader.Read())
                    { //ha ez sikertelen, jelezzük a hibát 0-val, ami nem megengedett érték az identity mezőnkön
                        return 0;
                    }
                    //| ID  |
                    //-------
                    //| 126 |

                    //Egy oszlopos a visszaadott rekordhalmaz,
                    //és az első oszlop a 0-s indexű
                    var id = (int)reader.GetDecimal(0);
                    return id;
                }
            }
        }
    }
}
