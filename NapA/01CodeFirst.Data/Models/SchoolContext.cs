using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01CodeFirst.Data.Models
{
    public class SchoolContext : DbContext
    {
        //Így induláskor keres egy SchoolContext nevű connectionString-et, ha van használja,
        //ha nincs akkor a SchoolContext az adatbázisnév
        //public SchoolContext() : base("SchoolContext")

        //Ha így írjuk: name=..., akkor ez a connectionStringet jelenti
        public SchoolContext() : base("name=SchoolContext")
        { }


        public DbSet<Teacher> Teachers { get; set; }
    }
}
