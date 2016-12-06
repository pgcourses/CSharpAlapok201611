using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04FuncActionLambda
{
    class Program
    {
        public delegate int NegyzetreEmelesDef(int x);

        static void Main(string[] args)
        {
            //Ez a delegate-tel rendelkezésre álló megoldás, 4 lépésből áll:

            //1. delegate definíció
            //2. függvény definíció
            //3. változó létrehozás és értékadás = híváslista feltöltése
            //4. híváslista meghívása
            NegyzetreEmelesDef negyzetHivaslista = NegyzetreEmeles;
            Console.WriteLine("Négyzet: {0}",negyzetHivaslista(2));

            //Ezek helyett kell egy egyszerűbb megoldás
            //A függvény definíció kiváltására szolgálnak a lambda kifejezések

            //Az előző módon létrehozott dolgot el tudjuk intézni így:
            //A lambda kifejezés (=>) baloldalán a paraméterlista
            //a jobb oldalán a kódblokk, vagyis a metódus törzse
            negyzetHivaslista = (x) => { return x * x; };
            //Ha egy kifejezéssel kell visszatérni, akkor nem kell kódblokk, csak a kifejezés
            //ha egy paraméterem van, nem kell zárójel a paraméterlista köré
            negyzetHivaslista = z => z * z;
            Console.WriteLine("Négyzet: {0}", negyzetHivaslista(2));

            //Ezzel az egy sorral kilőttük a 2-es és 3-as pontot.

            //Már csak az 1-es ponttal kellene valamit kezdeni.
            //Action<> és a Func<>
            //Az Action<> arra szolgál, hogy visszatérési érték nélküli (void) delegate definíciót hozzunk létre
            //A Func<> arra szolgál, hogy visszatérési értékkel rendelkező delegate definíciót hozzunk létre

            //A generikus típusparaméterekkel megadjuk a paraméterlistán szereplő változók típusát, és a legutolsó paraméter 
            //a visszatérési érték
            Func<int, int> negyzetHivaslista2 = z => z * z;
            //Ezzel kilőttük az 1-es pontot is, vagyis, az 1-2-3-as lépéseket egy sorban lebonyolítjuk

            Console.WriteLine("Négyzet: {0}", negyzetHivaslista2(2));

            //Ha egynél több paraméterünk van, akkor a lambda paramétereit zárójelbe kell tenni
            Func<int, int, string> szorzasHivaslista = (i, j) => string.Format("{0}",i * j);
            Console.WriteLine("Szorzas: {0}", szorzasHivaslista(2,3));

            //Ugyanezek a példák Action-nel
            Action<int> negyzetHivaslistaActionNel = k => Console.WriteLine("Négyzet az Action definícióban: {0}", k * k);
            negyzetHivaslistaActionNel(3);

            Action<int, int> szorzasHivaslistaActionNel = (a, b) => Console.WriteLine("Szorzás az Action-ben: {0}", a * b);
            szorzasHivaslistaActionNel(5, 6);

            Console.ReadLine();

        }

        static int NegyzetreEmeles(int x)
        {
            return x * x;
        }

    }
}
