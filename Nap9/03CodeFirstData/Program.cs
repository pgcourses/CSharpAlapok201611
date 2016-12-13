using _03CodeFirstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03CodeFirstData
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new SchoolContext();

            var teacher1 = new Teacher() { Firstname = "Gipsz", Lastname = "Jakab", ClassCode = "1/A" };
            var teacher2 = new Teacher() { Firstname = "Adat", Lastname = "Lajos", ClassCode = "2/B" };
            var teacher3 = new Teacher() { Firstname = "Néző", Lastname = "Tamás", ClassCode = "8/C" };
            db.Teachers.Add(teacher1);
            db.Teachers.Add(teacher2);
            db.Teachers.Add(teacher3);

            var t = new Teacher() { Firstname = "Nagy", Lastname = "Lajos", ClassCode = "1/A" };
            db.Teachers.Add(t);
            t = new Teacher() { Firstname = "Kis", Lastname = "Tamás", ClassCode = "2/B" };
            db.Teachers.Add(t);
            t = new Teacher() { Firstname = "Alma", Lastname = "Márton", ClassCode = "8/C" };
            db.Teachers.Add(t);

            db.SaveChanges();

            //LINQ példák: 
            //https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b
            //http://linqsamples.com/linq-to-objects/ordering/OrderByDescending
            //http://linq101.nilzorblog.com/linq101-lambda.php
            //http://www.c-sharpcorner.com/resources/388/101-linq-samples.aspx
            Console.WriteLine("A tanárok száma: {0}", db.Teachers.Count());

            var teachers1A = db.Teachers.Where(x => x.ClassCode == "1/A");

            //fontos, hogy az enumárátor ne a dbset-et kapja feladatként, hanem az
            //adatbázislekérdezést egy lépésben végrehajtva memóriában lévő adatokon
            //iteráljunk
            foreach (var t1a in teachers1A.ToList())
            {
                Console.WriteLine("Név: {0} {1}", t1a.Firstname, t1a.Lastname);
            }

            //Összeadhatunk bármit
            var sum = teachers1A.Sum(x => x.Id);

            var sum2 = db.Teachers
                         .Where(x => x.Firstname.Contains("a"))
                         .Sum(x => x.Id);

            Console.WriteLine("Az Id-k összege: {0}, {1}", sum, sum2);

            //db.Teachers.Remove(teacher1);
            //db.Teachers.RemoveRange(new Teacher[] { teacher2, teacher3 });

            //fontos, hogy az enumárátor ne a dbset-et kapja feladatként, hanem az
            //adatbázislekérdezést egy lépésben végrehajtva memóriában lévő adatokon
            //iteráljunk
            foreach (var teacher in db.Teachers.ToList())
            {
                db.Teachers.Remove(teacher);
            }

            db.SaveChanges();

            Console.ReadLine();
        }
    }
}
