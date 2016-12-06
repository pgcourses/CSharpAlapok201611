using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _05AsyncMukodes
{
    class Program
    {

        static void Main(string[] args)
        {
            //Híváslista létrehozása, amin a függvények a következők:
            //paraméterként egy int és egy sztringet vár, és visszaad egy DateTime-ot
            Func<int, string, DateTime> am = (ciklusok, nev) =>
            {
                Console.WriteLine("+->{0} elindult, ciklusok: {1}", nev, ciklusok);
                for (int i = 0; i < ciklusok; i++)
                {
                    Console.WriteLine("+-->{0} ciklus: {1}", nev, i);
                    Thread.Sleep(400);
                }
                Console.WriteLine("+->{0} végzett", nev);
                return DateTime.Now;
            };

            var eredmeny0 = am(3, "Szinkron hívás");
            Console.WriteLine("+Fő szál: szinkron hívás elindult");
            Console.WriteLine("+Fő szál: szinkron hívás végzett, eredmény: {0}", eredmeny0);

            Console.ReadLine();

        }
    }
}
