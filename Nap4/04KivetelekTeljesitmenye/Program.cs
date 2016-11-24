using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04KivetelekTeljesitmenye
{
    class Program
    {
        static void Main(string[] args)
        {

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception) {  }
            }

            Console.WriteLine("Eltelt idő: {0}", sw.ElapsedTicks);

            sw.Restart();

            for (int i = 0; i < 1000; i++)
            {
                //try
                //{
                //    throw new Exception();
                //}
                //catch (Exception) { }
            }

            Console.WriteLine("Eltelt idő: {0}", sw.ElapsedTicks);

            //Eltelt idő: 22877042
            //Eltelt idő: 9

            Console.ReadLine();

        }
    }
}
