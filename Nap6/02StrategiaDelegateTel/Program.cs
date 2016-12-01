using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02StrategiaDelegateTel
{
    class Program
    {
        static void Main(string[] args)
        {
            var adatOsztaly = new AdatOsztaly(adatok: new List<int>(new int[] { 1, 3, 6, 7, 9, 11, 20, 3, 6 }));

            //Ez a legszószátyárabb megoldás, három lépés
            //1. delegate definíció
            //2. függvény definíció
            //3. fogadni kell a delegate típust
            //4. A hívás
            var eredmeny = adatOsztaly.Muvelet(ListaOsszegzes);

            //Egy kicsit tömörebb
            //1. Kell egy függvény definíció
            //2. fogadni kell a delegate típust Func<> segítségével
            //3. Kell egy hívás
            var eredmeny2 = adatOsztaly.Muvelet2(ListaOsszegzes);

            //Ennél tömörebb pedig nincs
            //1. Fogadni kell a delegate típust Func<> segítségével
            //2. a hívásban a lambda segítségével beküldjük a műveletet (a stratégiánkat)

            //var eredmeny3 = adatOsztaly.Muvelet3((x) => { return x.Sum(); });
            //ez ugyanaz, csak mivel a kódblokkban egy kifejezés található, így a kódblokk és a return elhagyható
            var eredmeny3 = adatOsztaly.Muvelet3(x => x.Sum());

        }

        static int ListaOsszegzes(List<int> adatok)
        {
            return adatok.Sum();
        }

    }

    public class AdatOsztaly
    {
        public delegate int ListaMuveletDef(List<int> adatok);
        private List<int> adatok;

        public AdatOsztaly(List<int> adatok)
        {
            this.adatok = adatok;
        }

        public int Muvelet(ListaMuveletDef muvelet)
        {
            var muveletLista = muvelet;
            if(muveletLista==null)
            {
                return 0;
            }
            return muveletLista(adatok);
        }

        internal object Muvelet2(Func<List<int>, int> listaOsszegzes)
        {
            throw new NotImplementedException();
        }

        internal object Muvelet3(Func<List<int>, int> listaOsszegzes)
        {
            throw new NotImplementedException();
        }



    }

}
