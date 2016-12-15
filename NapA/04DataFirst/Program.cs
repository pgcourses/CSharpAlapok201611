using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04DataFirst
{
    /// <summary>
    /// DbFirst tutorial: http://www.entityframeworktutorial.net/entityframework6/introduction.aspx
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var db = new SchoolContext();
            foreach (var teacher in db.Teachers.ToList())
            {
                Console.WriteLine("{0}", teacher.FullName);
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
