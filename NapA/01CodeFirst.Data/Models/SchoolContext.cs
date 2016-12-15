using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A Code First migrations-hoz a Package Manager Console-ból ki kell adni az
/// enable-migrations parancsot
/// 
/// update-database: három konfiguráció:
/// 
/// DB Config <- Code config <- Model config
/// 
/// További információk:
/// https://coding.abel.nu/2012/02/using-entity-framework-to-create-a-database/
/// http://thedatafarm.com/?s=code+first
/// https://blog.oneunicorn.com/tag/code-first/
/// http://www.entityframeworktutorial.net/code-first
/// </summary>


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
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
