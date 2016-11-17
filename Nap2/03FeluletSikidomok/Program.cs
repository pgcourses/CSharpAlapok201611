using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// System   --------------------------> Linq
///                       |
///                       -------------> Text
///                       |
///                       -------------> Collection
///                       |      |
///                       |      ----------------------> Generic
///                       |
///                       -------------> Threading
///                              |
///                              ----------------------> Tasks
///

namespace _03FeluletSikidomok
{
    class Program
    {
        static void Main(string[] args)
        {
            Letrehozas();

            Console.ReadLine();
        }

        private static void Letrehozas()
        {
            var alap = new Alap("Ez itt a név", "Ez itt a cim");
            Console.WriteLine();

            var leszarmaztatott = new Leszarmaztatott();
            Console.WriteLine();

            var tovabbszarmaztatott = new TovabbSzarmaztatott();
        }

        class Alap
        {
            //Amikor példányosítunk egy objektumot, akkor a konstruktora fut le.
            //
            //ha nincs ilyen implementálva, akkor a fordító
            //automatikusan létrehoz egy public paraméter nélküli 
            //un. alapértelmezett konstruktort
            
            //ilyet:
            //public Alap() { }

            string Nev;
            string Cim;


            public Alap()
            {
                Console.WriteLine("Alap konstruktor");
            }

            //konstruktor overloading
            public Alap(string nev) : this() //ez pedig a paraméter nélküli konstruktort hívja
            {
                Console.WriteLine("Alap konstruktor 2: {0}", nev);
                Nev = nev;
            }

            //további konstruktor overloading, ami átadja a már meglévő konstruktornak 
            //a feladatnak azt a részét amit ő már kezel
            public Alap(string nev, string cim) : this(nev) //ez meghívja a paraméterrel az előző konstruktort
            {
                Console.WriteLine("Alap konstruktor 3: {0}, {1}", nev, cim);
                Cim = cim;
            }

            //Finalizer, ami akkor fut, ha az osztálypéldány befejezte életét
            //Őt magát nem lehet hívni, a futtatókörnyezet hívja meg valamikor
            //részletesen a Garbage Collector és az IDisposable tartalomnál
            //megbeszéljük
            ~Alap()
            {
                Console.WriteLine("Alap finalizer");
            }
        }

        class Leszarmaztatott : Alap
        {
            public Leszarmaztatott()
            {
                Console.WriteLine("Leszarmaztatott konstruktor");
            }

            ~Leszarmaztatott()
            {
                Console.WriteLine("Leszármaztatott finalizer");
            }
        }

        class TovabbSzarmaztatott : Leszarmaztatott
        {
            public TovabbSzarmaztatott()
            {
                Console.WriteLine("TovabbSzarmaztatott konstruktor");
            }

            ~TovabbSzarmaztatott()
            {
                Console.WriteLine("TovabbSzarmaztatott finalizer");
            }
        }
    }
}
