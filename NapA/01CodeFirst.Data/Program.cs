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
        static void Main(string[] args)
        {
            var db = new SchoolContext();
            Console.WriteLine(db.Teachers.Count(x=>x.ClassCode=="1/A"));
            Console.ReadLine();
        }
    }
}
