using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Tipusok
{
    //A fordítás célja egy MSIL nyelvű állomány létrehozása.
    //Az MSIL programot .NET

    // .NET keretrendszer
    // CLR - ez futtatja az MSIL nyelvű
    //      Tranzakciók
    //      Memóriakezelés
    //      Többszálú végrehajtás
    // .NET Framework osztálykönyvtár
    //      Előre megírt rengeteg függvény, amit a programozó használhat


    // Memóriakezelés
    // Kétféle memória: STACK (Verem) és HEAP (Halom)
    // A verem kezelése mindig hézagmentes.
    //      HEAP                      STACK    
    //  |   x     |               |           |
    //  |     x   |               |           |
    //  |      x1-|---------------|->y 20     | 
    //  |         |    |          |           |
    //  |      x2-|----           |           |

    //  | se1.Ertek     |               |             |
    //  | se1.Hiv ------|---------------|->y.Ertek 20 | 
    //  |               |        |      |             |
    //  | se2.Ertek     |        |      |             |
    //  | se2.Hiv ------|---------      |             |
    //  |         

    //


    class Program
    {
        static void Main(string[] args)
        {

            // Értéktípusok: értékadás esetén mindig új példány keletkezik
            var ertektipus = 5;
            var ertektipus2 = ertektipus;

            ertektipus = 6;

            Console.WriteLine("Értékek {0}, {1}", ertektipus, ertektipus2);

            //var sb = new StringBuilder();

            //A Szöveg is értéktípusként viselkedik
            var szoveg = "egyik szöveg";
            var szoveg2 = szoveg;

            szoveg = "ez már egy másik szöveg";

            Console.WriteLine("Első szöveg: {0}", szoveg);
            Console.WriteLine("Második szöveg: {0}", szoveg2);

            //Hivatkozástípus értékadás esetén, nem másol új példányt, hanem az eredeti hivatkozás
            //kerül az értékadás során az új változóba.

            var hivatkozas1 = new HivatkozasTipus();
            hivatkozas1.Ertek = 10;

            var hivatkozas2 = hivatkozas1;

            hivatkozas1.Ertek = 20;

            Console.WriteLine("Hivatkozás1: {0}", hivatkozas1.Ertek);
            Console.WriteLine("Hivatkozás2: {0}", hivatkozas2.Ertek);

            //Saját értéktípus tesztelése

            var sajatertektipus1 = new ErtekTipus();
            sajatertektipus1.Ertek = 10;

            sajatertektipus1.Hiv = new HivatkozasTipus();
            sajatertektipus1.Hiv.Ertek = 10;

            var sajatertektipus2 = sajatertektipus1;
            sajatertektipus1.Ertek = 20;
            sajatertektipus1.Hiv.Ertek = 20;

            Console.WriteLine("SajatErtek1: {0}", sajatertektipus1.Ertek);
            Console.WriteLine("SajatErtek2: {0}", sajatertektipus2.Ertek);
            Console.WriteLine("SajatErtek1-hiv: {0}", sajatertektipus1.Hiv.Ertek);
            Console.WriteLine("SajatErtek2-hiv: {0}", sajatertektipus2.Hiv.Ertek);

            ////FIGYELEM!!!!
            ////csak értéktípusú property-k viselkednek értéktípusként

            Console.ReadLine();
        }

    }

    /// <summary>
    /// Minden class hivatkozástípus
    /// </summary>
    class HivatkozasTipus
    {
        public int Ertek { get; set; }
    }

    /// <summary>
    /// Saját értéktípus létrehozása
    /// </summary>
    struct ErtekTipus
    {
        public int Ertek { get; set; }

        public HivatkozasTipus Hiv { get; set; }

    }
}
