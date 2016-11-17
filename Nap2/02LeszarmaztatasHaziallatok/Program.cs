using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02LeszarmaztatasHaziallatok
{
    class Program
    {
        /// |      Kutya                                     |.(le van származtatva)>|           Haziallat                                   |
        /// |------------------------------------------------|                       |-------------------------------------------------------|
        /// |                                                |                       |                                                       |
        /// |     [Koszon()] --------(a leszármaztatás miatt ez a haziallatot hívja)---------->Koszon()                                      |
        /// |                                                |                       |                                                       |
        /// |  new Enekel() (a háziállat énekel fv-el a new miatt nincs kapcsolat)   |         Enekel()                                      |
        /// |                                                |                       |                                                       |
        /// | override Beszel() <----(Ha a kutyanak a haziallat feluletén keresztül)-- virtual Beszel()                                      |
        /// |                                                |                       |           |                                           |
        /// |          base.Beszel()-------->---------->------------------>------------->---------                                           |
        /// |                                                |                       |                                                       |
        /// |                                                |                       |                                                       |
        /// |                                                |                       |                                                       |
        /// |                                                |                       |                                                       |
        /// |                                                |                       |                                                       |
        /// |                                                |                       |                                                       |






        static void Main(string[] args)
        {
            /// A függvényeket a kutya oldaláról hívom
            /// ezt azzal érem el, hogy a változóm típusa Kutya
            Kutya kutya = new Kutya();
            kutya.Koszon();
            kutya.Enekel();
            kutya.Beszel();

            Console.WriteLine();

            /// Eredmény:
            /// A háziállat köszön
            /// A kutya énekel
            /// A kutya beszél
            /// 


            /// A függvényeket a háziállat oldaláról hívom
            /// ezt azzal érem el, hogy a változóm típusa Háziállat
            Haziallat haziallat = new Kutya();
            haziallat.Koszon();
            haziallat.Enekel();
            haziallat.Beszel();

            Console.WriteLine();
            ///Eredmény:
            ///A háziállat köszön
            ///A háziállat énekel
            ///A kutya beszél
            ///

            //Direktben ki tudok jelölni felületet:
            //A háziállat típusú változónak veszem a Kutya felületét
            //Akkor ismét a Kutyát szólítom meg
            ((Kutya)haziallat).Koszon();
            ((Kutya)haziallat).Enekel();
            ((Kutya)haziallat).Beszel();

            ///Eredmény:
            ///A háziállat köszön
            ///A kutya énekel
            ///A kutya beszél
            ///A háziállat beszél
            ///Ugyanaz, mint legelőször
            ///

            //Ez futásidőben derül ki, hogy végrehajtható-e?
            //Ez nem hajtható végre, unable to cast exception
            //var haziallat2 = new Haziallat();
            //((Kutya)haziallat2).Beszel();

            Console.WriteLine();

            //Milyen eszközeink vannak, és hogy érdemes a típuskonverziót elvégezni?
            //Az object minden osztály ősosztálya, így minden objektum beletehető
            //az object típusú változóba

            //object o = new Kutya();

            //Ellenpróba
            object o = new Haziallat();

            //De hogy tudom innen kivenni?

            //1. megoldás: 
            //ellenőrzöm, hogy van-e ilyen felülete
            ObjectbolKutya1(o);

            //2. megoldás:
            //as kulcsszóval megpróbálok konvertálni
            ObjectbolKutya2(o);

            //3. megoldás
            //megpróbálom átkonvertálni, vagy sikerül vagy nem
            //mivel ez elszállhat, így betesszük egy hibakezelő blokkba
            ObjectbolKutya3(o);

            //nézzük meg az egyes teljesítményeket
            var olist = new object[1000];
            for (int i = 0; i < 1000; i++)
            {
                if (i % 2 == 0)
                {
                    olist[i] = new Haziallat();
                }
                else
                {
                    olist[i] = new Kutya();
                }
            }

            var sw = new System.Diagnostics.Stopwatch();

            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                ObjectbolKutya1(olist[i]);
            }
            Console.WriteLine("1-es módszer: {0}", sw.ElapsedTicks);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                ObjectbolKutya2(olist[i]);
            }
            Console.WriteLine("2-es módszer: {0}", sw.ElapsedTicks);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                ObjectbolKutya3(olist[i]);
            }
            Console.WriteLine("3as módszer: {0}", sw.ElapsedTicks);


            Console.ReadLine();
        }

        private static void ObjectbolKutya3(object o)
        {
            try
            {
                Kutya k3 = (Kutya)o;
                //Console.WriteLine("Ő egy kutya (try)");
                Kutya k = (Kutya)o;
                //k.Enekel();
            }
            catch (Exception)
            {
                //Console.WriteLine("Ez sajnos nem kutya (try)");
            }
        }

        private static void ObjectbolKutya2(object o)
        {
            Kutya k2 = o as Kutya;
            if (k2 != null)
            { //sikerült a konverzió
                //Console.WriteLine("Ő egy kutya (as)");
                Kutya k = (Kutya)o;
                //k.Enekel();
            }
            else
            {
                //Console.WriteLine("Ez sajnos nem kutya (as)");
            }
        }

        private static void ObjectbolKutya1(object o)
        {
            if (o is Kutya)
            {
                //Console.WriteLine("Ő egy kutya (is)");
                Kutya k = (Kutya)o;
                //k.Enekel();
            }
            else
            {
                //Console.WriteLine("Ez sajnos nem kutya (is)");
            }
        }

        class Haziallat
        {
            public void Koszon()
            {
                Console.WriteLine("A háziállat köszön");
            }

            public void Enekel()
            {
                Console.WriteLine("A háziállat énekel");
            }

            //A virtual kulcsszó jelzi, hogy a leszármaztatott osztályban ezt felül tudom definiálni
            public virtual void Beszel()
            {
                Console.WriteLine("A háziállat beszél");
            }
        }

        class Kutya : Haziallat //Amiből leszármaztatok ősosztálynak hívom
        {
            //ha ugyanolyan névvel hozzuk létre, akkor ez ugyanazt jelenti, mintha new kulcsszóval hoztuk volna létre
            public void Enekel()
            {
                Console.WriteLine("A kutya énekel");
            }

            //Az override-dal felülírjuk az eredeti implementációt
            public override void Beszel()
            {
                Console.WriteLine("A kutya beszél");
                //Az ősosztály függvényét a base kulcsszóval érem el
                base.Beszel();
            }
        }

        class Macska : Haziallat
        {

        }
    }
}
