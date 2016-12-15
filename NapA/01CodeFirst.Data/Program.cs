using _01CodeFirst.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01CodeFirst.Data
{
    class Program
    {
        //CRUD: Create, Read, Update, Delete
        static void Main(string[] args)
        {
            var db = new SchoolContext();
            foreach (var teacher in db.Teachers.ToList())
            {
                Console.WriteLine("{0} {1}", teacher.Firstname, teacher.Lastname);
                Console.WriteLine(" -> Subject: {0}", teacher.Subject.Name);
                foreach (var student in teacher.Students.ToList())
                {
                    Console.WriteLine(" ---> Student: {0}", student.Name);
                }

            }
            Console.ReadLine();
        }
    }
}
