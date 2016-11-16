using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Tipusok
{
    class Program
    {
        // A fordítás célja egy MSIL nyelvű állomány létrehozása

        ///    --------               ---------
        ///    | C#   |      ------>  | MSIL  | - - - - - - - - -
        ///    --------        |      --|-|-|--                 |
        ///    ----------      |                                ˇ
        ///    | VB.NET |    --|                            |       |
        ///    ----------      |                            |-|-|-|-|
        ///    -------         |                      ----------------------
        ///    | PHP |       --                       | .NET keretrendszer |
        ///    -------                            ------------------------------
        ///                                       |                            |
        ///                                       |     Operációs rendszer     |
        ///                                       |     (Windows/Linux)        |
        ///                                       |                            |
        ///                                       ------------------------------

        /// 
        /// .NET keretrendszer
        /// - Osztálykönyvtár
        ///     Rengeteg előre megírt függvény/objektum
        /// 
        /// - CLR (Common Language Runtime)
        ///    Memóriakezelés
        ///    Tranzakciók
        ///    Többszálúság
        /// 

        /// Alkalmazás -> folyamat (process) -> szálak (thread) -> verem (stack)
        ///
        /// |---------------------------|            |------------------------------|
        /// | (hívási)verem/(call)stack |            | halom/heap                   |
        /// |---------------------------|            |------------------------------|
        /// |                           |      ------|>[0]                          |   
        /// | ertek1                    |     |      |                              |
        /// | ertek2                    |     |      |                              |
        /// |                           |     |      |                              |
        /// | hivatkozas1 --------------|-----|      |                              |
        /// |                           |     |      |                              |
        /// | hivatkozas2 --------------|-----       |                              |
        /// |                           |            |                              |
        /// | sajatertek1.Ertek         |            |                              |
        /// |            .Hivatkozas ---|------------|-- SajatHivatkozas.Ertek(0)   |
        /// | sajatertek2.Ertek         |    |       |                              |
        /// |            .Hivatkozas ---|-----       |                              |
        /// |                           |         ---|--->SajatHivatkozas.Ertek(0)  |
        /// |                           |        |   |                              |
        /// |                           |        |   |                              |
        /// | sajathivatkozas1 ---------|---------   |                              |
        /// |                           |        |   |                              |
        /// | sajathivatkozas2 ---------|--------    |                              |
        /// |                           |            |                              |
        /// |                           |            |                              |
        /// | szoveg1 ------------------|------------|--->String("Eredeti szöveg")  |
        /// |                           |            |                              |
        /// | szoveg2 ------------------|------------|--->String("Eredeti szöveg")  |
        /// |                           |            |                              |
        /// |                           |            |  StringBuilder()             |
        /// |                           |            |                              |
        /// |                           |            |                              |


        static void Main(string[] args)
        {

            //Értéktípus: ebben az esetben értékadáskor a változó értéke lemásolódik, egy új példány keletkezik
            var ertek1 = 0;

            //Ez ugyanaz, mintha ezeket írtam volna:
            int ertek = 0;
            //vagy
            int ertek0;
            ertek0 = 0;
            //sőt, ugyanaz, mint ez!!!
            Int32 ertekobjektum1 = new Int32();

            //primitív típusok: számok, logikai érték, enum

            //létrehozunk egy új változót az első változó segítségével
            var ertek2 = ertek1;
            //Majd az első változó értékét módosítjuk
            ertek1 = 10;
            //Nézzük meg az eredményt
            Console.WriteLine("Érték1: {0}, Érték2: {1}", ertek1, ertek2);
            //Eredmény: 
            //Érték1: 10, Érték2: 0 //tehát az ertek2 és ez ertek1 két egymástól független változó

            //Referenciatípus: értékadáskor a változóra mutató referencia adódik át
            var hivatkozas1 = new int[] { 0 };  //létrehozok egy egyelemű egész számokból álló tömböt.

            var hivatkozas2 = hivatkozas1;

            hivatkozas1[0] = 10;
            Console.WriteLine("Hivatkozás1: {0}, Hivatkozás2: {1}", hivatkozas1[0], hivatkozas2[0]);
            //Eredmény: Hivatkozás1: 10, Hivatkozás2: 10 //tehát a két érték együtt mozog

            var sajatertek1 = new SajatErtekTipus();
            sajatertek1.Ertek = 0;
            sajatertek1.Hivatkozas = new SajatHivatkozasTipus();
            sajatertek1.Hivatkozas.Ertek = 0;

            var sajatertek2 = sajatertek1;
            sajatertek1.Ertek = 10;
            sajatertek1.Hivatkozas.Ertek = 10;

            Console.WriteLine("SajátÉrték1: {0}, SajátÉrték2: {1}", sajatertek1.Ertek, sajatertek2.Ertek);
            //Eredmény: SajátÉrték1: 10, SajátÉrték2: 0 //vagyis, a két érték önálló saját tulajdonsággal rendelkezik, egymástól független
            Console.WriteLine("SajátÉrték1.Hivatkozas.Ertek: {0}, SajátÉrték2.Hivatkozas.Ertek: {1}", 
                sajatertek1.Hivatkozas.Ertek, sajatertek2.Hivatkozas.Ertek);
            //Eredmény: SajátÉrték1.Hivatkozas.Ertek: 10, SajátÉrték2.Hivatkozas.Ertek: 10 //Vagyis, a hivatkozástípus
                                                                                           //jellege akkor sem változik, 
                                                                                           //ha értéktípusba van csomagolva

            var sajathivatkozas1 = new SajatHivatkozasTipus();
            sajathivatkozas1.Ertek = 0;

            var sajathivatkozas2 = sajathivatkozas1;
            sajathivatkozas1.Ertek = 10;
            Console.WriteLine("SajátHivatkozás1: {0}, SajátHivatkozás2: {1}", sajathivatkozas1.Ertek, sajathivatkozas2.Ertek);
            //Eredmény: SajátHivatkozás1: 10, SajátHivatkozás2: 10 //tehát a két érték együtt mozog

            var szoveg1 = "Eredeti szöveg";
            var szoveg2 = szoveg1;

            szoveg1 = "Módosított szöveg";
            Console.WriteLine("Szöveg1: {0}, Szöveg2: {1}", szoveg1, szoveg2);
            //Eredmény: Szöveg1: Módosított szöveg, Szöveg2: Eredeti szöveg // vagyis a string az értéktípusként VISELKEDIK

            ////Tehát ilyet ne csináljunk
            //var szoveg = "";
            //for (int i = 0; i < 10000000; i++)
            //{
            //    szoveg = szoveg + "valami új";
            //}

            //var sb = new StringBuilder();
            //for (int i = 0; i < 10000000; i++)
            //{
            //    sb.Append("valami új");
            //}
            ////Így tudok az eredményhez hozzáférni
            //Console.WriteLine(sb.ToString()); 

            Console.ReadKey();
        }
    }

    class SajatHivatkozasTipus
    {
        public int Ertek;
    }

    struct SajatErtekTipus
    {
        public int Ertek;
        public SajatHivatkozasTipus Hivatkozas;
    }

}
