using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("1 kg  kenyér");
            //Console.WriteLine("10 dkg párizsi");
            //Console.WriteLine("1 l tej");
            //Console.WriteLine("1 kg liszt");
            //Console.WriteLine("1 még valami");
            //Console.WriteLine("meg még valami");
            //Console.WriteLine("és egy kis édesség");

            foreach (var listaElem in BevasarloLista())
            {
                Console.WriteLine(listaElem);
            }

            Console.ReadLine();

            //var list = new List<string>();
            //var list2 = new List<Sikidom>();

        }

        private static IEnumerable<string> BevasarloLista()
        {
            yield return "1 kg  kenyér";
            yield return "10 dkg párizsi";
            yield return "1 l tej";
            yield return "1 kg liszt";
            yield return "1 még valami";
            yield return "meg még valami";
            yield return "és egy kis édesség";
        }

    }
}